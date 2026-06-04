// LC1116 - Print Zero Even Odd
// Three threads share one ZeroEvenOdd object.
// Thread A calls zero() – prints 0 before every number.
// Thread B calls even() – prints even numbers.
// Thread C calls odd()  – prints odd  numbers.
// Output for n=5: "0102030405"

/*
1116. Print Zero Even Odd
Medium
Topics
premium lock icon
Companies
You have a function printNumber that can be called with an integer parameter and prints it to the console.

For example, calling printNumber(7) prints 7 to the console.
You are given an instance of the class ZeroEvenOdd that has three functions: zero, even, and odd. The same instance of ZeroEvenOdd will be passed to three different threads:

Thread A: calls zero() that should only output 0's.
Thread B: calls even() that should only output even numbers.
Thread C: calls odd() that should only output odd numbers.
Modify the given class to output the series "010203040506..." where the length of the series must be 2n.

Implement the ZeroEvenOdd class:

ZeroEvenOdd(int n) Initializes the object with the number n that represents the numbers that should be printed.
void zero(printNumber) Calls printNumber to output one zero.
void even(printNumber) Calls printNumber to output one even number.
void odd(printNumber) Calls printNumber to output one odd number.
 

Example 1:

Input: n = 2
Output: "0102"
Explanation: There are three threads being fired asynchronously.
One of them calls zero(), the other calls even(), and the last one calls odd().
"0102" is the correct output.
Example 2:

Input: n = 5
Output: "0102030405"
 

Constraints:

1 <= n <= 1000 
 */

using System.Text;

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1116_ZeroEvenOdd
    {
		private int n;
		private int nextNonZeroNumber = 1;
		private readonly SemaphoreSlim _gateZero = new(1, 1);
		private readonly SemaphoreSlim _gateOdd = new(0, 1);
		private readonly SemaphoreSlim _gateEven = new(0, 1);

		public LC1116_ZeroEvenOdd(int n)
		{
			this.n = n;
		}

		// printNumber(x) outputs "x", where x is an integer.
		public void Zero(Action<int> printNumber)
		{
			for (var i = 1; i <= n; i++)
			{
				_gateZero.Wait();
				//_oddPrinted.WaitOne();
				printNumber(0);
				if (nextNonZeroNumber % 2 == 0)
				{
					_gateEven.Release(); // release even
				}
				else
				{
					_gateOdd.Release(); // release odd
				}
			}
		}

		public void Even(Action<int> printNumber)
		{
			for (var i = 2; i <= n; i += 2)
			{
				_gateEven.Wait();
				printNumber(nextNonZeroNumber);
				Interlocked.Increment(ref nextNonZeroNumber);
				_gateZero.Release(); //release zero
			}
		}

		public void Odd(Action<int> printNumber)
		{
			for (var i = 1; i <= n; i += 2)
			{
				_gateOdd.Wait();
				printNumber(nextNonZeroNumber);
				Interlocked.Increment(ref nextNonZeroNumber);
				_gateZero.Release(); //release zero
			}
		}
	}

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1116_Tests
    {
        private static string Run(int n)
        {
            var sb  = new StringBuilder();
            var obj = new LC1116_ZeroEvenOdd(n);
            var lk  = new object();

            void Print(int v) { lock (lk) sb.Append(v); }

            var t1 = Task.Run(() => obj.Zero(Print));
            var t2 = Task.Run(() => obj.Even(Print));
            var t3 = Task.Run(() => obj.Odd(Print));

            Task.WaitAll(t1, t2, t3);
            return sb.ToString();
        }

        [Theory]
        [InlineData(1, "01")]
        [InlineData(2, "0102")]
        [InlineData(3, "010203")]
        [InlineData(5, "0102030405")]
        [InlineData(6, "010203040506")]
        public void Output_MatchesExpected(int n, string expected) =>
            Assert.Equal(expected, Run(n));

        [Fact]
        public void RepeatedRuns_AreConsistent()
        {
            for (int i = 0; i < 20; i++)
                Assert.Equal("0102030405", Run(5));
        }
    }
}
