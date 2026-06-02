// LC1226 - The Dining Philosophers
// Five philosophers sit at a table with five forks (one between each pair).
// To eat, a philosopher needs both adjacent forks.
// Avoid deadlock, starvation, and race conditions.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────
    // Strategy: Allow at most 4 philosophers to try picking up forks simultaneously.
    // This breaks the circular wait condition and prevents deadlock.

    public class LC1226_DiningPhilosophers
    {
        private readonly SemaphoreSlim[] _forks;
        // Limit concurrent diners to n-1 to prevent deadlock
        private readonly SemaphoreSlim _table;

        public LC1226_DiningPhilosophers()
        {
            _forks = new SemaphoreSlim[5];
            for (int i = 0; i < 5; i++)
                _forks[i] = new SemaphoreSlim(1, 1);
            _table = new SemaphoreSlim(4, 4); // at most 4 of 5 can try concurrently
        }

        // Called by philosopher `n` (0..4)
        //   pickLeftFork  – action to call when left fork acquired
        //   pickRightFork – action to call when right fork acquired
        //   eat           – action to call while eating
        //   putLeftFork   – action to call when putting left fork down
        //   putRightFork  – action to call when putting right fork down
        public void WantsToEat(
            int philosopher,
            Action pickLeftFork,
            Action pickRightFork,
            Action eat,
            Action putLeftFork,
            Action putRightFork)
        {
            int left  = philosopher;
            int right = (philosopher + 1) % 5;

            _table.Wait();          // sit at the table (at most 4)
            try
            {
                _forks[left].Wait();
                pickLeftFork();

                _forks[right].Wait();
                pickRightFork();

                eat();

                putRightFork();
                _forks[right].Release();

                putLeftFork();
                _forks[left].Release();
            }
            finally
            {
                _table.Release();   // leave the table
            }
        }
    }

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1226_Tests
    {
        [Fact]
        public void AllPhilosophers_EatOnce_NoDeadlock()
        {
            var dp      = new LC1226_DiningPhilosophers();
            var ate     = new bool[5];
            var tasks   = new Task[5];

            for (int i = 0; i < 5; i++)
            {
                int idx = i;
                tasks[idx] = Task.Run(() =>
                    dp.WantsToEat(idx,
                        () => { }, () => { },
                        () => { Thread.Sleep(5); ate[idx] = true; },
                        () => { }, () => { }));
            }

            bool completed = Task.WaitAll(tasks, TimeSpan.FromSeconds(10));
            Assert.True(completed, "Deadlock detected – not all tasks finished in time");
            Assert.All(ate, a => Assert.True(a));
        }

        [Fact]
        public void AllPhilosophers_EatMultipleTimes_NoDeadlock()
        {
            var dp      = new LC1226_DiningPhilosophers();
            var counts  = new int[5];
            const int meals = 10;

            var tasks = new Task[5];
            for (int i = 0; i < 5; i++)
            {
                int idx = i;
                tasks[idx] = Task.Run(() =>
                {
                    for (int m = 0; m < meals; m++)
                        dp.WantsToEat(idx,
                            () => { }, () => { },
                            () => Interlocked.Increment(ref counts[idx]),
                            () => { }, () => { });
                });
            }

            bool completed = Task.WaitAll(tasks, TimeSpan.FromSeconds(30));
            Assert.True(completed, "Deadlock detected");
            for (int i = 0; i < 5; i++)
                Assert.Equal(meals, counts[i]);
        }

        [Fact]
        public void Fork_NeverHeldByTwoPhilosophers_Simultaneously()
        {
            var dp           = new LC1226_DiningPhilosophers();
            var forkOwners   = new int[5];
            for (int f = 0; f < 5; f++) forkOwners[f] = -1;
            var lockObj      = new object();
            bool conflict    = false;
            const int meals  = 20;

            var tasks = new Task[5];
            for (int i = 0; i < 5; i++)
            {
                int idx = i;
                tasks[idx] = Task.Run(() =>
                {
                    for (int m = 0; m < meals; m++)
                    {
                        int left  = idx;
                        int right = (idx + 1) % 5;

                        dp.WantsToEat(idx,
                            () => { lock (lockObj) { if (forkOwners[left]  != -1) conflict = true; forkOwners[left]  = idx; } },
                            () => { lock (lockObj) { if (forkOwners[right] != -1) conflict = true; forkOwners[right] = idx; } },
                            () => Thread.Sleep(1),
                            () => { lock (lockObj) forkOwners[left]  = -1; },
                            () => { lock (lockObj) forkOwners[right] = -1; });
                    }
                });
            }

            Task.WaitAll(tasks, TimeSpan.FromSeconds(30));
            Assert.False(conflict, "Two philosophers held the same fork simultaneously");
        }
    }
}
