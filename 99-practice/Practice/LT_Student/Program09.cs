using System;
using System.Collections.Generic;
using System.Linq;

// solution to exercise 9
class Program09
{
	static void MainX()
	{
		List<int> numbers = Enumerable.Range(1, 10_000_000).ToList();

		Console.WriteLine("Starting parallel computation...");

		// TODO: Modifique a expressão LINQ abaixo usando PLINQ (.AsParallel())
		// para distribuir a carga pesada de processamento entre os cores da CPU de forma balanceada.
		var processed = numbers
			.AsParallel()
			.Where(n => n % 2 != 0)
			.Select(n => Math.Sqrt(n))
			.ToList();

		Console.WriteLine($"Computation finished. Total processed items: {processed.Count}");
	}
}