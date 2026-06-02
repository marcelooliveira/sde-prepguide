using System;
using System.Collections.Generic;

//new solution take for exercise 5
class Program05
{
	static void MainX()
	{
		int[] balances = { 1020, 500, 2000, 1500, 3000, 80, 4200 };
		int targetSum = 1580; // Par esperado: 1500 + 80 (índices 3 e 5)

		// TODO: Implemente um algoritmo O(n) para achar e imprimir os dois índices.
		var balanceAndIndex = new Dictionary<int, int>();

		var result = new int[2];

		for (int i = 0; i < balances.Length; i++)
		{
			var balance = balances[i];
			var delta = targetSum - balance;
			if (balanceAndIndex.TryGetValue(delta, out var otherIndex))
			{
				result[0] = i;
				result[1] = otherIndex;
			}
			balanceAndIndex[balance] = i;
		}			

		Console.WriteLine($"The two indices are {result[0]} and {result[1]}");

		Console.WriteLine("Algorithm execution finished.");
	}
}