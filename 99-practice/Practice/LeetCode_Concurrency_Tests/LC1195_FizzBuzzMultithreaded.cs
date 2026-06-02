// LC1195 - FizzBuzz Multithreaded
// Four threads share a FizzBuzz object for n numbers:
//   fizz()     – prints "fizz"  for multiples of 3 (not 5)
//   buzz()     – prints "buzz"  for multiples of 5 (not 3)
//   fizzbuzz() – prints "fizzbuzz" for multiples of both 3 and 5
//   number()   – prints the number itself otherwise

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
