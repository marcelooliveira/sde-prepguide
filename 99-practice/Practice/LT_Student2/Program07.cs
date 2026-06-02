using System;
using System.Threading.Tasks;

// new solution take to exercise 7
class Program07
{
	private static int _consolidatedBalance = 0;

	static async Task MainX()
	{
		Task[] tasks = new Task[10];

		for (int i = 0; i < 10; i++)
		{
			tasks[i] = Task.Run(() =>
			{
				for (int j = 0; j < 100_000; j++)
				{
					// TODO: Este ponto causa uma Race Condition. 
					// Corrija usando primitivas de sincronização rápidas (ex: classe Interlocked).

					// old code
					//_consolidatedBalance++;

					// new code
					Interlocked.Increment(ref _consolidatedBalance);
				}
			});
		}

		await Task.WhenAll(tasks);
		Console.WriteLine($"Expected: 1000000 | Actual Balance: {_consolidatedBalance}");
	}
}