// LC1115 - Print FooBar Alternately
// Two threads alternately call foo() and bar() n times, printing "foobar" n times.

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1115_FooBar
    {
        private readonly int _n;
        private readonly SemaphoreSlim _fooReady = new SemaphoreSlim(1, 1); // foo goes first
        private readonly SemaphoreSlim _barReady = new SemaphoreSlim(0, 1);

        public LC1115_FooBar(int n) => _n = n;

        public void Foo(Action printFoo)
        {
            for (int i = 0; i < _n; i++)
            {
                _fooReady.Wait();
                printFoo();
                _barReady.Release();
            }
        }

        public void Bar(Action printBar)
        {
            for (int i = 0; i < _n; i++)
            {
                _barReady.Wait();
                printBar();
                _fooReady.Release();
            }
        }
    }

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1115_Tests
    {
        private static string Run(int n)
        {
            var sb  = new StringBuilder();
            var obj = new LC1115_FooBar(n);

            var t1 = Task.Run(() => obj.Foo(() => { lock (sb) sb.Append("foo"); }));
            var t2 = Task.Run(() => obj.Bar(() => { lock (sb) sb.Append("bar"); }));

            Task.WaitAll(t1, t2);
            return sb.ToString();
        }

        [Theory]
        [InlineData(1, "foobar")]
        [InlineData(2, "foobarfoobar")]
        [InlineData(5, "foobarfoobarfoobarfoobarfoobar")]
        public void Output_MatchesExpected(int n, string expected) =>
            Assert.Equal(expected, Run(n));

        [Fact]
        public void LargeN_CompletesCorrectly()
        {
            string result = Run(1000);
            Assert.Equal(8000, result.Length); // "foobar" = 6 chars * 1000
            for (int i = 0; i < 1000; i++)
                Assert.Equal("foobar", result.Substring(i * 6, 6));
        }

        [Fact]
        public void RepeatedRuns_AlwaysAlternate()
        {
            for (int i = 0; i < 20; i++)
                Assert.Equal("foobarfoobarfoobar", Run(3));
        }
    }
}
