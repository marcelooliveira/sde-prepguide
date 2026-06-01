using System;
using System.Collections.Generic;

class Program05
{
	static void MainX()
	{
		int[] balances = { 1020, 500, 2000, 1500, 3000, 80, 4200 };
		int targetSum = 1580; // Par esperado: 1500 + 80 (índices 3 e 5)

		// TODO: Implemente um algoritmo O(n) para achar e imprimir os dois índices.

		//Answer to Problem 5:

		var result = new int[2];
		var valueXIndex = new Dictionary<int, int>();
		for (int i = 0; i < balances.Length; i++)
		{
			var balance = balances[i];
			var delta = targetSum - balance;
			if (valueXIndex.TryGetValue(delta, out int value))
			{
				result[0] = value;
				result[1] = i;
				break;
			}
			valueXIndex[balance] = i;
		}

		Console.WriteLine($"{result[0]} and {result[1]}");


		Console.WriteLine("Algorithm execution finished.");
	}
}