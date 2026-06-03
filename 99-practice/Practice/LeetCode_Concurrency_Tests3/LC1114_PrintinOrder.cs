// LC1114 - Print in Order
// Given three functions first(), second(), third() to be called by three threads,
// ensure they execute in order: first -> second -> third.
/*
1114. Print in Order
Easy
Topics
premium lock icon
Companies
Suppose we have a class:

public class Foo {
  public void first() { print("first"); }
  public void second() { print("second"); }
  public void third() { print("third"); }
}
The same instance of Foo will be passed to three different threads. Thread A will call first(), thread B will call second(), and thread C will call third(). Design a mechanism and modify the program to ensure that second() is executed after first(), and third() is executed after second().

Note:

We do not know how the threads will be scheduled in the operating system, even though the numbers in the input seem to imply the ordering. The input format you see is mainly to ensure our tests' comprehensiveness.

 

Example 1:

Input: nums = [1,2,3]
Output: "firstsecondthird"
Explanation: There are three threads being fired asynchronously. The input [1,2,3] means thread A calls first(), thread B calls second(), and thread C calls third(). "firstsecondthird" is the correct output.
Example 2:

Input: nums = [1,3,2]
Output: "firstsecondthird"
Explanation: The input [1,3,2] means thread A calls first(), thread B calls third(), and thread C calls second(). "firstsecondthird" is the correct output.
 

Constraints:

nums is a permutation of [1, 2, 3].
*/

namespace LeetCode.Concurrency
{
    // ─── Solution ───────────────────────────────────────────────────────────────

    public class LC1114_Foo
    {
        public LC1114_Foo() { }

        public void First(Action printFirst)
        {
            printFirst();
        }

        public void Second(Action printSecond)
        {
            printSecond();
        }

        public void Third(Action printThird)
        {
            printThird();
        }
    }

    // ─── Tests ───────────────────────────────────────────────────────────────────

    public class LC1114_Tests
    {
        [Theory]
        [InlineData(new[] { 1, 2, 3 })]
        [InlineData(new[] { 1, 3, 2 })]
        [InlineData(new[] { 2, 1, 3 })]
        [InlineData(new[] { 2, 3, 1 })]
        [InlineData(new[] { 3, 1, 2 })]
        [InlineData(new[] { 3, 2, 1 })]
        public void Output_IsAlways_FirstSecondThird(int[] order)
        {
            var foo    = new LC1114_Foo();
            var result = new System.Collections.Concurrent.ConcurrentQueue<int>();

            Action a1 = () => { Thread.Sleep(10); foo.First(()  => result.Enqueue(1)); };
            Action a2 = () => { Thread.Sleep(10); foo.Second(() => result.Enqueue(2)); };
            Action a3 = () => { Thread.Sleep(10); foo.Third(()  => result.Enqueue(3)); };

            Action[] actions = { a1, a2, a3 };

            var tasks = new Task[3];
            for (int i = 0; i < 3; i++)
            {
                int idx = order[i] - 1;
                tasks[i] = Task.Run(actions[idx]);
            }

            Task.WaitAll(tasks);

            Assert.Equal(new[] { 1, 2, 3 }, result.ToArray());
        }

        [Fact]
        public void RunMultipleTimes_AlwaysOrdered()
        {
            for (int run = 0; run < 20; run++)
            {
                var foo    = new LC1114_Foo();
                var result = new System.Collections.Concurrent.ConcurrentQueue<int>();

                var t1 = Task.Run(() => foo.First(()  => result.Enqueue(1)));
                var t3 = Task.Run(() => foo.Third(()  => result.Enqueue(3)));
                var t2 = Task.Run(() => foo.Second(() => result.Enqueue(2)));

                Task.WaitAll(t1, t2, t3);

                Assert.Equal(new[] { 1, 2, 3 }, result.ToArray());
            }
        }
    }
}
