// LC1242 - Web Crawler Multithreaded
// Given a URL and an HtmlParser, crawl all URLs that share the same hostname
// using multiple threads. Return all crawled URLs.
// Constraint: only follow URLs whose hostname matches the startUrl's hostname.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace LeetCode.Concurrency
{
    // ─── HtmlParser interface (provided by LeetCode) ─────────────────────────────

    public interface IHtmlParser
    {
        IList<string> GetUrls(string url);
    }

    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1242_Solution
    {
        public IList<string> Crawl(string startUrl, IHtmlParser htmlParser)
        {
            string hostname = GetHostname(startUrl);

            var visited = new ConcurrentDictionary<string, byte>();
            visited.TryAdd(startUrl, 0);

            // Channel-style work queue using a bag + countdown
            var queue   = new ConcurrentQueue<string>();
            var pending = 0; // number of URLs currently being processed

            queue.Enqueue(startUrl);
            Interlocked.Increment(ref pending);

            using var allDone = new ManualResetEventSlim(false);

            void ProcessUrl(string url)
            {
                var links = htmlParser.GetUrls(url);
                foreach (var link in links)
                {
                    if (GetHostname(link) == hostname && visited.TryAdd(link, 0))
                    {
                        Interlocked.Increment(ref pending);
                        queue.Enqueue(link);
                    }
                }

                if (Interlocked.Decrement(ref pending) == 0)
                    allDone.Set();
            }

            // Spin up worker threads
            int threadCount = Math.Min(Environment.ProcessorCount * 2, 16);
            var cts         = new CancellationTokenSource();

            for (int i = 0; i < threadCount; i++)
            {
                Task.Run(() =>
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        if (queue.TryDequeue(out string url))
                            ProcessUrl(url);
                        else if (pending == 0)
                            break;
                        else
                            Thread.SpinWait(10);
                    }
                }, cts.Token);
            }

            allDone.Wait();
            cts.Cancel();

            return visited.Keys.ToList();
        }

        private static string GetHostname(string url)
        {
            // "http://news.yahoo.com/news?p=1" -> "news.yahoo.com"
            var withoutScheme = url.Substring(url.IndexOf("//") + 2);
            var slashIdx      = withoutScheme.IndexOf('/');
            return slashIdx == -1 ? withoutScheme : withoutScheme.Substring(0, slashIdx);
        }
    }

    // ─── Test helpers ─────────────────────────────────────────────────────────────

    public class MockHtmlParser : IHtmlParser
    {
        private readonly Dictionary<string, List<string>> _graph;
        private int _callCount = 0;

        public int CallCount => _callCount;

        public MockHtmlParser(Dictionary<string, List<string>> graph) => _graph = graph;

        public IList<string> GetUrls(string url)
        {
            Interlocked.Increment(ref _callCount);
            Thread.Sleep(2); // simulate latency
            return _graph.TryGetValue(url, out var links) ? links : new List<string>();
        }
    }

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1242_Tests
    {
        private static MockHtmlParser BuildParser(Dictionary<string, List<string>> graph)
            => new MockHtmlParser(graph);

        [Fact]
        public void SimpleGraph_ReturnsAllSameHostUrls()
        {
            var graph = new Dictionary<string, List<string>>
            {
                ["http://news.yahoo.com/news"]         = new() { "http://news.yahoo.com/news/today", "http://news.yahoo.com/politics" },
                ["http://news.yahoo.com/news/today"]   = new() { "http://news.yahoo.com/news" },
                ["http://news.yahoo.com/politics"]     = new() { "http://news.yahoo.com/news" },
            };

            var solution = new LC1242_Solution();
            var parser   = BuildParser(graph);
            var result   = solution.Crawl("http://news.yahoo.com/news", parser);

            var expected = new HashSet<string>
            {
                "http://news.yahoo.com/news",
                "http://news.yahoo.com/news/today",
                "http://news.yahoo.com/politics"
            };

            Assert.Equal(expected, new HashSet<string>(result));
        }

        [Fact]
        public void CrossDomainLinks_AreIgnored()
        {
            var graph = new Dictionary<string, List<string>>
            {
                ["http://news.yahoo.com/news"] = new()
                {
                    "http://news.yahoo.com/sport",
                    "http://news.google.com/news"   // different hostname
                },
                ["http://news.yahoo.com/sport"] = new()
                {
                    "http://www.facebook.com"       // different hostname
                }
            };

            var solution = new LC1242_Solution();
            var result   = solution.Crawl("http://news.yahoo.com/news", BuildParser(graph));

            Assert.DoesNotContain("http://news.google.com/news", result);
            Assert.DoesNotContain("http://www.facebook.com", result);
            Assert.Contains("http://news.yahoo.com/sport", result);
        }

        [Fact]
        public void CyclicGraph_EachUrlVisitedOnce()
        {
            var graph = new Dictionary<string, List<string>>
            {
                ["http://a.com/1"] = new() { "http://a.com/2" },
                ["http://a.com/2"] = new() { "http://a.com/3" },
                ["http://a.com/3"] = new() { "http://a.com/1" }, // cycle
            };

            var parser   = BuildParser(graph);
            var solution = new LC1242_Solution();
            var result   = solution.Crawl("http://a.com/1", parser);

            // No duplicates
            Assert.Equal(result.Count, new HashSet<string>(result).Count);
            // All 3 visited
            Assert.Equal(3, result.Count);
            // Parser called exactly once per URL
            Assert.Equal(3, parser.CallCount);
        }

        [Fact]
        public void IsolatedStartUrl_ReturnsOnlyItself()
        {
            var graph  = new Dictionary<string, List<string>>(); // no outbound links
            var result = new LC1242_Solution().Crawl("http://solo.com/page", BuildParser(graph));
            Assert.Single(result);
            Assert.Equal("http://solo.com/page", result[0]);
        }
    }
}
