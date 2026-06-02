// LC1226 - The Dining Philosophers
// Five philosophers sit at a table with five forks (one between each pair).
// To eat, a philosopher needs both adjacent forks.
// Avoid deadlock, starvation, and race conditions.

/*
1226. The Dining Philosophers
Medium
Topics
premium lock icon
Companies
Five silent philosophers sit at a round table with bowls of spaghetti. Forks are placed between each pair of adjacent philosophers.

Each philosopher must alternately think and eat. However, a philosopher can only eat spaghetti when they have both left and right forks. Each fork can be held by only one philosopher and so a philosopher can use the fork only if it is not being used by another philosopher. After an individual philosopher finishes eating, they need to put down both forks so that the forks become available to others. A philosopher can take the fork on their right or the one on their left as they become available, but cannot start eating before getting both forks.

Eating is not limited by the remaining amounts of spaghetti or stomach space; an infinite supply and an infinite demand are assumed.

Design a discipline of behaviour (a concurrent algorithm) such that no philosopher will starve; i.e., each can forever continue to alternate between eating and thinking, assuming that no philosopher can know when others may want to eat or think.



The problem statement and the image above are taken from wikipedia.org

 

The philosophers' ids are numbered from 0 to 4 in a clockwise order. Implement the function void wantsToEat(philosopher, pickLeftFork, pickRightFork, eat, putLeftFork, putRightFork) where:

philosopher is the id of the philosopher who wants to eat.
pickLeftFork and pickRightFork are functions you can call to pick the corresponding forks of that philosopher.
eat is a function you can call to let the philosopher eat once he has picked both forks.
putLeftFork and putRightFork are functions you can call to put down the corresponding forks of that philosopher.
The philosophers are assumed to be thinking as long as they are not asking to eat (the function is not being called with their number).
Five threads, each representing a philosopher, will simultaneously use one object of your class to simulate the process. The function may be called for the same philosopher more than once, even before the last call ends.

 

Example 1:

Input: n = 1
Output: [[3,2,1],[3,1,1],[3,0,3],[3,1,2],[3,2,2],[4,2,1],[4,1,1],[2,2,1],[2,1,1],[1,2,1],[2,0,3],[2,1,2],[2,2,2],[4,0,3],[4,1,2],[4,2,2],[1,1,1],[1,0,3],[1,1,2],[1,2,2],[0,1,1],[0,2,1],[0,0,3],[0,1,2],[0,2,2]]
Explanation:
n is the number of times each philosopher will call the function.
The output array describes the calls you made to the functions controlling the forks and the eat function, its format is:
output[i] = [a, b, c] (three integers)
- a is the id of a philosopher.
- b specifies the fork: {1 : left, 2 : right}.
- c specifies the operation: {1 : pick, 2 : put, 3 : eat}.
 */

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
