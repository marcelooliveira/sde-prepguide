using System;
using System.Linq;
using System.Collections.Generic;

class Program09
{
	static void MainX()
	{
		var dataStream = Enumerable.Range(1, 5_000_000).ToList();

		// TODO: Transforme o pipeline LINQ abaixo usando PLINQ (AsParallel) 
		// para processar as computações pesadas dividindo entre os cores da CPU.
		var processedData = dataStream
			.Where(x => x % 3 == 0)
			.Select(x => Math.Sqrt(x))
			.ToList();

		Console.WriteLine($"Processing pipeline streaming finished. Total items: {processedData.Count}");
	}
}