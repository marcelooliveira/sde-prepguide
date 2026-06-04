// LC1195 - FizzBuzz Multithreaded
// Four threads share a FizzBuzz object for n numbers:
//   fizz()     – prints "fizz"  for multiples of 3 (not 5)
//   buzz()     – prints "buzz"  for multiples of 5 (not 3)
//   fizzbuzz() – prints "fizzbuzz" for multiples of both 3 and 5
//   number()   – prints the number itself otherwise

/*

Code
Testcase
Testcase
Test Result
1195. Fizz Buzz Multithreaded
Medium
Topics
premium lock icon
Companies
You have the four functions:

printFizz that prints the word "fizz" to the console,
printBuzz that prints the word "buzz" to the console,
printFizzBuzz that prints the word "fizzbuzz" to the console, and
printNumber that prints a given integer to the console.
You are given an instance of the class FizzBuzz that has four functions: fizz, buzz, fizzbuzz and number. The same instance of FizzBuzz will be passed to four different threads:

Thread A: calls fizz() that should output the word "fizz".
Thread B: calls buzz() that should output the word "buzz".
Thread C: calls fizzbuzz() that should output the word "fizzbuzz".
Thread D: calls number() that should only output the integers.
Modify the given class to output the series [1, 2, "fizz", 4, "buzz", ...] where the ith token (1-indexed) of the series is:

"fizzbuzz" if i is divisible by 3 and 5,
"fizz" if i is divisible by 3 and not 5,
"buzz" if i is divisible by 5 and not 3, or
i if i is not divisible by 3 or 5.
Implement the FizzBuzz class:

FizzBuzz(int n) Initializes the object with the number n that represents the length of the sequence that should be printed.
void fizz(printFizz) Calls printFizz to output "fizz".
void buzz(printBuzz) Calls printBuzz to output "buzz".
void fizzbuzz(printFizzBuzz) Calls printFizzBuzz to output "fizzbuzz".
void number(printNumber) Calls printnumber to output the numbers.
 

Example 1:

Input: n = 15
Output: [1,2,"fizz",4,"buzz","fizz",7,8,"fizz","buzz",11,"fizz",13,14,"fizzbuzz"]
Example 2:

Input: n = 5
Output: [1,2,"fizz",4,"buzz"]
 

Constraints:

1 <= n <= 50
 */

using System.Text;

namespace LeetCode.Concurrency
{
	// ─── Solution ───────────────────────────────────────────────────────────────

	public class LC1195_FizzBuzz
	{
		private int n;
		private readonly AutoResetEvent _fizzEvent = new(true); // AutoResetEvent = turnstile
		private readonly AutoResetEvent _buzzEvent = new(false);
		private readonly AutoResetEvent _fizzBuzzEvent = new(false);
		private readonly AutoResetEvent _numberEvent = new(false);

		private readonly Func<int, bool> _isIsFizz = (int i) => (i % 3 == 0 && i % 5 != 0);
		private readonly Func<int, bool> _isIsBuzz = (int i) => (i % 3 != 0 && i % 5 == 0);
		private readonly Func<int, bool> _isIsFizzBuzz = (int i) => (i % 3 == 0 && i % 5 == 0);
		private readonly Func<int, bool> _isNumber = (int i) => (i % 3 != 0 && i % 5 != 0);

		public LC1195_FizzBuzz(int n)
		{
			this.n = n;

		}

		// printFizz() outputs "fizz".
		public void Fizz(Action printFizz)
		{
			for (var i = 1; i <= n; i++)
			{
				_fizzEvent.WaitOne();
				if (_isIsFizz(i))
					printFizz();
				_buzzEvent.Set();
			}
		}

		// printBuzzz() outputs "buzz".
		public void Buzz(Action printBuzz)
		{
			for (var i = 1; i <= n; i++)
			{
				_buzzEvent.WaitOne();
				if (_isIsBuzz(i))
					printBuzz();
				_fizzBuzzEvent.Set();
			}
		}

		// printFizzBuzz() outputs "fizzbuzz".
		public void Fizzbuzz(Action printFizzBuzz)
		{

			for (var i = 1; i <= n; i++)
			{
				_fizzBuzzEvent.WaitOne();
				if (_isIsFizzBuzz(i))
					printFizzBuzz();
				_numberEvent.Set();
			}
		}

		// printNumber(x) outputs "x", where x is an integer.
		public void Number(Action<int> printNumber)
		{
			for (var i = 1; i <= n; i++)
			{
				_numberEvent.WaitOne();

				if (_isNumber(i))
					printNumber(i);
				_fizzEvent.Set();
			}
		}
	}

	// ─── Tests ───────────────────────────────────────────────────────────────────

	public class LC1195_Tests
	{
		private static string Run(int n)
		{
			var fb = new LC1195_FizzBuzz(n);
			var sb = new StringBuilder();
			var lk = new object();

			void Append(string s) { lock (lk) sb.Append(s + " "); }

			var t1 = Task.Run(() => fb.Number(i => Append(i.ToString())));
			var t2 = Task.Run(() => fb.Fizz(() => Append("fizz")));
			var t3 = Task.Run(() => fb.Buzz(() => Append("buzz")));
			var t4 = Task.Run(() => fb.Fizzbuzz(() => Append("fizzbuzz")));

			Task.WaitAll(t1, t2, t3, t4);
			return sb.ToString().TrimEnd();
		}

		private static string Expected(int n)
		{
			var parts = new System.Collections.Generic.List<string>();
			for (int i = 1; i <= n; i++)
			{
				if (i % 15 == 0) parts.Add("fizzbuzz");
				else if (i % 3 == 0) parts.Add("fizz");
				else if (i % 5 == 0) parts.Add("buzz");
				else parts.Add(i.ToString());
			}
			return string.Join(" ", parts);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(3)]
		[InlineData(5)]
		[InlineData(15)]
		[InlineData(16)]
		[InlineData(20)]
		public void Output_MatchesSingleThread(int n) =>
			Assert.Equal(Expected(n), Run(n));

		[Fact]
		public void RepeatedRuns_AreConsistent()
		{
			string exp = Expected(15);
			for (int i = 0; i < 20; i++)
				Assert.Equal(exp, Run(15));
		}
	}
}
