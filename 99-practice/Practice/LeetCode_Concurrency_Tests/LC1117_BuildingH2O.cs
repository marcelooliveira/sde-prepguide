// LC1117 - Building H2O
// Threads are waiting to call hydrogen() or oxygen().
// Release hydrogen twice and oxygen once to form water.
// All hydrogen threads must call hydrogen() before any oxygen thread calls oxygen() for the same molecule.

using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1117_H2O
    {
        // Allow at most 2 H's before an O is required
        private readonly SemaphoreSlim _hSem = new SemaphoreSlim(2, 2);
        private readonly SemaphoreSlim _oSem = new SemaphoreSlim(0, 1);
        private int _hCount = 0;

        public LC1117_H2O() { }

        public void Hydrogen(Action releaseHydrogen)
        {
            _hSem.Wait();
            releaseHydrogen();
            if (Interlocked.Increment(ref _hCount) == 2)
            {
                Interlocked.Exchange(ref _hCount, 0);
                _oSem.Release();
            }
        }

        public void Oxygen(Action releaseOxygen)
        {
            _oSem.Wait();
            releaseOxygen();
            _hSem.Release(2);
        }
    }

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1117_Tests
    {
        private static string Run(string input)
        {
            var h2o    = new LC1117_H2O();
            var result = new ConcurrentQueue<char>();
            var tasks  = new Task[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                tasks[i] = c == 'H'
                    ? Task.Run(() => h2o.Hydrogen(() => result.Enqueue('H')))
                    : Task.Run(() => h2o.Oxygen(()   => result.Enqueue('O')));
            }

            Task.WaitAll(tasks);
            return new string(result.ToArray());
        }

        private static bool IsValidH2O(string output)
        {
            if (output.Length % 3 != 0) return false;
            // Sort chars in each group of 3: should be "HHO"
            var arr = output.ToCharArray();
            Array.Sort(arr); // all H's first then O's

            int molecules = output.Length / 3;
            int hExpected = molecules * 2;
            int oExpected = molecules;

            int hCount = 0, oCount = 0;
            foreach (char c in arr)
            {
                if (c == 'H') hCount++;
                else if (c == 'O') oCount++;
            }

            return hCount == hExpected && oCount == oExpected;
        }

        [Theory]
        [InlineData("HOH")]
        [InlineData("OOHH")]
        [InlineData("HHOHHO")]
        [InlineData("HHOHHOHHOHHO")]
        public void Output_FormsValidMolecules(string input) =>
            Assert.True(IsValidH2O(Run(input)));

        [Fact]
        public void LargeInput_FormsCorrectNumberOfMolecules()
        {
            // 30 H's + 15 O's = 15 molecules
            string input = new string('H', 30) + new string('O', 15);
            string result = Run(input);
            Assert.True(IsValidH2O(result));
            Assert.Equal(45, result.Length);
        }

        [Fact]
        public void RepeatedRuns_AlwaysValid()
        {
            for (int i = 0; i < 15; i++)
                Assert.True(IsValidH2O(Run("HHOHHOHHOHHO")));
        }
    }
}
