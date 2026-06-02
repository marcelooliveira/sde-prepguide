using System;
using System.Collections.Generic;

//new solution take to exercise 1
class Program01
{
	static void MainX()
	{
		// Simulando a carga pesada de dados inserida pelo entrevistador
		List<long> transactionIds = GenerateMassiveIds(5_000_000);

		Console.WriteLine("Processing starts now...");
		var watch = System.Diagnostics.Stopwatch.StartNew();

		// TODO: Implemente a deduplicação e filtragem eficiente.
		// O objetivo é obter apenas os IDs pares e únicos com a menor complexidade de tempo/espaço possível.
		// Dica: Inicialize a capacidade da estrutura para evitar resizes na memória.

		var uniqueEvenNums = new HashSet<long>(transactionIds.Count / 2);

		for (int i = 0; i < transactionIds.Count; i++)
		{
			var transactionId = transactionIds[i];
			if ((transactionId & 1) == 0)
			{
				uniqueEvenNums.Add(transactionId);
			}
		}

		watch.Stop();
		Console.WriteLine($"uniqueEvenNums.Count : {uniqueEvenNums.Count}");
		Console.WriteLine($"Finished in: {watch.ElapsedMilliseconds}ms");
	}

	static List<long> GenerateMassiveIds(int count)
	{
		var list = new List<long>(count);
		var rand = new Random(42);
		for (int i = 0; i < count; i++)
			list.Add(rand.Next(1, count / 2));
		return list;
	}
}