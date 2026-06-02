// LC1188 - Design Bounded Blocking Queue
// Implement a thread-safe bounded blocking queue:
//   - enqueue(element): blocks if full
//   - dequeue(): blocks if empty, returns front element
//   - size(): returns current number of elements

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1188_BoundedBlockingQueue
    {
        private readonly Queue<int>      _queue;
        private readonly int             _capacity;
        private readonly SemaphoreSlim   _notFull;   // slots available to write
        private readonly SemaphoreSlim   _notEmpty;  // items available to read
        private readonly object          _lock = new object();

        public LC1188_BoundedBlockingQueue(int capacity)
        {
            _capacity = capacity;
            _queue    = new Queue<int>(capacity);
            _notFull  = new SemaphoreSlim(capacity, capacity);
            _notEmpty = new SemaphoreSlim(0, capacity);
        }

        public void Enqueue(int element)
        {
            _notFull.Wait();                     // block if full
            lock (_lock) _queue.Enqueue(element);
            _notEmpty.Release();
        }

        public int Dequeue()
        {
            _notEmpty.Wait();                    // block if empty
            int val;
            lock (_lock) val = _queue.Dequeue();
            _notFull.Release();
            return val;
        }

        public int Size()
        {
            lock (_lock) return _queue.Count;
        }
    }

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1188_Tests
    {
        [Fact]
        public void SingleThread_EnqueueDequeue_Works()
        {
            var q = new LC1188_BoundedBlockingQueue(3);
            q.Enqueue(1);
            q.Enqueue(2);
            q.Enqueue(3);
            Assert.Equal(3, q.Size());
            Assert.Equal(1, q.Dequeue());
            Assert.Equal(2, q.Dequeue());
            Assert.Equal(1, q.Size());
        }

        [Fact]
        public void Concurrent_ProducerConsumer_AllItemsReceived()
        {
            const int itemCount = 100;
            var q       = new LC1188_BoundedBlockingQueue(10);
            var results = new System.Collections.Concurrent.ConcurrentBag<int>();

            var producer = Task.Run(() =>
            {
                for (int i = 0; i < itemCount; i++) q.Enqueue(i);
            });

            var consumer = Task.Run(() =>
            {
                for (int i = 0; i < itemCount; i++) results.Add(q.Dequeue());
            });

            Task.WaitAll(producer, consumer);

            Assert.Equal(0, q.Size());
            Assert.Equal(itemCount, results.Count);
        }

        [Fact]
        public void EnqueueBlocks_WhenFull()
        {
            var q    = new LC1188_BoundedBlockingQueue(1);
            var cts  = new CancellationTokenSource();
            q.Enqueue(42);

            var blocked = Task.Run(() =>
            {
                q.Enqueue(99); // should block until consumer dequeues
            });

            // Give it time to block
            Thread.Sleep(100);
            Assert.False(blocked.IsCompleted, "Enqueue should block when full");

            q.Dequeue(); // unblock
            blocked.Wait(TimeSpan.FromSeconds(2));
            Assert.True(blocked.IsCompleted);
        }

        [Fact]
        public void DequeueBlocks_WhenEmpty()
        {
            var q = new LC1188_BoundedBlockingQueue(3);

            var blocked = Task.Run(() => q.Dequeue()); // blocks

            Thread.Sleep(100);
            Assert.False(blocked.IsCompleted, "Dequeue should block when empty");

            q.Enqueue(7);
            Assert.Equal(7, blocked.Result);
        }

        [Fact]
        public void MultipleProducersConsumers_NoItemLost()
        {
            const int total     = 200;
            const int producers = 4;
            const int consumers = 4;

            var q       = new LC1188_BoundedBlockingQueue(20);
            var results = new System.Collections.Concurrent.ConcurrentBag<int>();

            var pTasks = new Task[producers];
            var cTasks = new Task[consumers];

            for (int p = 0; p < producers; p++)
            {
                int start = p * (total / producers);
                int end   = start + (total / producers);
                pTasks[p] = Task.Run(() =>
                {
                    for (int i = start; i < end; i++) q.Enqueue(i);
                });
            }

            for (int c = 0; c < consumers; c++)
                cTasks[c] = Task.Run(() =>
                {
                    for (int i = 0; i < total / consumers; i++) results.Add(q.Dequeue());
                });

            Task.WaitAll(pTasks);
            Task.WaitAll(cTasks);

            Assert.Equal(total, results.Count);
            Assert.Equal(0, q.Size());
        }
    }
}
