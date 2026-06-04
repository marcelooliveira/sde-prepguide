// LC1117 - Building H2O
// Threads are waiting to call hydrogen() or oxygen().
// Release hydrogen twice and oxygen once to form water.
// All hydrogen threads must call hydrogen() before any oxygen thread calls oxygen() for the same molecule.

/*

Code
Testcase
Testcase
Test Result
1117. Building H2O
Medium
Topics
premium lock icon
Companies
There are two kinds of threads: oxygen and hydrogen. Your goal is to group these threads to form water molecules.

There is a barrier where each thread has to wait until a complete molecule can be formed. Hydrogen and oxygen threads will be given releaseHydrogen and releaseOxygen methods respectively, which will allow them to pass the barrier. These threads should pass the barrier in groups of three, and they must immediately bond with each other to form a water molecule. You must guarantee that all the threads from one molecule bond before any other threads from the next molecule do.

In other words:

If an oxygen thread arrives at the barrier when no hydrogen threads are present, it must wait for two hydrogen threads.
If a hydrogen thread arrives at the barrier when no other threads are present, it must wait for an oxygen thread and another hydrogen thread.
We do not have to worry about matching the threads up explicitly; the threads do not necessarily know which other threads they are paired up with. The key is that threads pass the barriers in complete sets; thus, if we examine the sequence of threads that bind and divide them into groups of three, each group should contain one oxygen and two hydrogen threads.

Write synchronization code for oxygen and hydrogen molecules that enforces these constraints.

 

Example 1:

Input: water = "HOH"
Output: "HHO"
Explanation: "HOH" and "OHH" are also valid answers.
Example 2:

Input: water = "OOHHHH"
Output: "HHOHHO"
Explanation: "HOHHHO", "OHHHHO", "HHOHOH", "HOHHOH", "OHHHOH", "HHOOHH", "HOHOHH" and "OHHOHH" are also valid answers.
 

Constraints:

3 * n == water.length
1 <= n <= 20
water[i] is either 'H' or 'O'.
There will be exactly 2 * n 'H' in water.
There will be exactly n 'O' in water.
 */

using System.Collections.Concurrent;

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1117_H2O
    {
		private readonly SemaphoreSlim _gateH = new(1);
		private readonly SemaphoreSlim _gateO = new(0);
		private int hCount = 0;
		
        public LC1117_H2O() { }

        public void Hydrogen(Action releaseHydrogen)
        {
			_gateH.Wait();
            Interlocked.Increment(ref hCount);

			// releaseHydrogen() outputs "H". Do not change or remove this line.
			releaseHydrogen();
            if (hCount % 2 == 0)
                _gateO.Release();
            else
                _gateH.Release();
		}

        public void Oxygen(Action releaseOxygen)
        {
			_gateO.Wait();

			// releaseOxygen() outputs "O". Do not change or remove this line.
			releaseOxygen();
			_gateH.Release();
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
