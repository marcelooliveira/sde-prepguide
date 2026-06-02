// LC1114 - Print in Order
// Given three functions first(), second(), third() to be called by three threads,
// ensure they execute in order: first -> second -> third.

using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1114_Foo
    {
        private readonly SemaphoreSlim _semFirst  = new SemaphoreSlim(0, 1);
        private readonly SemaphoreSlim _semSecond = new SemaphoreSlim(0, 1);

        public LC1114_Foo() { }

        public void First(Action printFirst)
        {
            printFirst();
            _semFirst.Release();
        }

        public void Second(Action printSecond)
        {
            _semFirst.Wait();
            printSecond();
            _semSecond.Release();
        }

        public void Third(Action printThird)
        {
            _semSecond.Wait();
            printThird();
        }
    }

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1114_Tests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3 })]
        [InlineData(new[] { 1, 3, 2 })]
        [InlineData(new[] { 2, 1, 3 })]
        [InlineData(new[] { 2, 3, 1 })]
        [InlineData(new[] { 3, 1, 2 })]
        [InlineData(new[] { 3, 2, 1 })]
        public void Output_IsAlways_FirstSecondThird(int[] order)
        {
            var foo    = new LC1114_Foo();
            var result = new System.Collections.Concurrent.ConcurrentQueue<int>();

            Action a1 = () => { Thread.Sleep(10); foo.First(()  => result.Enqueue(1)); };
            Action a2 = () => { Thread.Sleep(10); foo.Second(() => result.Enqueue(2)); };
            Action a3 = () => { Thread.Sleep(10); foo.Third(()  => result.Enqueue(3)); };

            Action[] actions = { a1, a2, a3 };

            var tasks = new Task[3];
            for (int i = 0; i < 3; i++)
            {
                int idx = order[i] - 1;
                tasks[i] = Task.Run(actions[idx]);
            }

            Task.WaitAll(tasks);

            Assert.Equal(new[] { 1, 2, 3 }, result.ToArray());
        }

        [Fact]
        public void RunMultipleTimes_AlwaysOrdered()
        {
            for (int run = 0; run < 20; run++)
            {
                var foo    = new LC1114_Foo();
                var result = new System.Collections.Concurrent.ConcurrentQueue<int>();

                var t1 = Task.Run(() => foo.First(()  => result.Enqueue(1)));
                var t3 = Task.Run(() => foo.Third(()  => result.Enqueue(3)));
                var t2 = Task.Run(() => foo.Second(() => result.Enqueue(2)));

                Task.WaitAll(t1, t2, t3);

                Assert.Equal(new[] { 1, 2, 3 }, result.ToArray());
            }
        }
    }
}
