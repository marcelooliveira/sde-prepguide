using System;
using System.Threading.Tasks;

class Program07
{
	private static int _sharedAccountBalance = 0;

	static async Task MainX()
	{
		Task[] tasks = new Task[10];

		for (int i = 0; i < 10; i++)
		{
			tasks[i] = Task.Run(() =>
			{
				for (int j = 0; j < 100_000; j++)
				{
					// TODO: Este trecho causa Race Condition. Corrija-o da forma mais leve possível
					// sem causar Deadlocks ou travamento excessivo de CPU.
					_sharedAccountBalance++;
				}
			});
		}

		await Task.WhenAll(tasks);
		Console.WriteLine($"Expected Balance: 1000000 | Actual Balance: {_sharedAccountBalance}");
	}
}