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

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1195_FizzBuzz
    {
        private readonly int _n;
        private int _current = 1;
        private readonly object _lock = new object();

        // Signals: one semaphore per "type" of number
        private readonly SemaphoreSlim _numSem  = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _fizzSem = new SemaphoreSlim(0, 1);
        private readonly SemaphoreSlim _buzzSem = new SemaphoreSlim(0, 1);
        private readonly SemaphoreSlim _fbSem   = new SemaphoreSlim(0, 1);

        public LC1195_FizzBuzz(int n) => _n = n;

        private void ReleaseNext(int val)
        {
            if (val > _n) { _numSem.Release(); _fizzSem.Release(); _buzzSem.Release(); _fbSem.Release(); }
            else if (val % 15 == 0) _fbSem.Release();
            else if (val % 3  == 0) _fizzSem.Release();
            else if (val % 5  == 0) _buzzSem.Release();
            else                     _numSem.Release();
        }

        public void Number(Action<int> printNumber)
        {
            while (true)
            {
                _numSem.Wait();
                if (_current > _n) return;
                printNumber(_current);
                ReleaseNext(++_current);
            }
        }

        public void Fizz(Action printFizz)
        {
            while (true)
            {
                _fizzSem.Wait();
                if (_current > _n) return;
                printFizz();
                ReleaseNext(++_current);
            }
        }

        public void Buzz(Action printBuzz)
        {
            while (true)
            {
                _buzzSem.Wait();
                if (_current > _n) return;
                printBuzz();
                ReleaseNext(++_current);
            }
        }

        public void FizzBuzz(Action printFizzBuzz)
        {
            while (true)
            {
                _fbSem.Wait();
                if (_current > _n) return;
                printFizzBuzz();
                ReleaseNext(++_current);
            }
        }
    }

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1195_Tests
    {
        private static string Run(int n)
        {
            var fb  = new LC1195_FizzBuzz(n);
            var sb  = new StringBuilder();
            var lk  = new object();

            void Append(string s) { lock (lk) sb.Append(s + " "); }

            var t1 = Task.Run(() => fb.Number(i  => Append(i.ToString())));
            var t2 = Task.Run(() => fb.Fizz(()   => Append("fizz")));
            var t3 = Task.Run(() => fb.Buzz(()   => Append("buzz")));
            var t4 = Task.Run(() => fb.FizzBuzz(() => Append("fizzbuzz")));

            Task.WaitAll(t1, t2, t3, t4);
            return sb.ToString().TrimEnd();
        }

        private static string Expected(int n)
        {
            var parts = new System.Collections.Generic.List<string>();
            for (int i = 1; i <= n; i++)
            {
                if      (i % 15 == 0) parts.Add("fizzbuzz");
                else if (i % 3  == 0) parts.Add("fizz");
                else if (i % 5  == 0) parts.Add("buzz");
                else                   parts.Add(i.ToString());
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
