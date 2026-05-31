using System;
using System.Collections.Generic;

class Program01
{
	static void Main()
	{
		// Simulando a carga pesada de dados inserida pelo entrevistador
		List<long> transactionIds = GenerateMassiveIds(10_000_000);

		Console.WriteLine("Processing starts now...");
		var watch = System.Diagnostics.Stopwatch.StartNew();

		// TODO: Implemente a deduplicação e filtragem eficiente aqui.
		// O objetivo é obter apenas os IDs pares e únicos com a menor complexidade de tempo/espaço possível.

		watch.Stop();
		Console.WriteLine($"Finished in: {watch.ElapsedMilliseconds}ms");
	}

	static List<long> GenerateMassiveIds(int count)
	{
		var list = new List<long>(count);
		var rand = new Random(42);
		for (int i = 0; i < count; i++)
			list.Add(rand.Next(1, count / 2)); // Forçando duplicatas deliberadas
		return list;
	}
}