using System;

class Program03
{
	static void MainX()
	{
		string rawPayload = "LOG|2026-05-31|TX_99887766|SUCCESS";

		// TODO: Use ReadOnlySpan<char> e os métodos .AsSpan(), .IndexOf() e .Slice()
		// para isolar o trecho "TX_99887766" com custo zero de alocação de memória.

		//Solution to the Exercise 3

		var newPayload = rawPayload.AsSpan();
		var targetCol = 2;
		var currentCol = 0;
		var right = newPayload.Length;

		do
		{
			var i = newPayload.IndexOf("|");
			right = i == -1 ? right : i;

			if (currentCol == targetCol)
				newPayload = newPayload.Slice(0, right);
			else
				newPayload = newPayload.Slice(right + 1, newPayload.Length - right - 1);

			currentCol++;
		}
		while (currentCol <= targetCol);

		Console.WriteLine($"Isolated slice: {newPayload}");

		Console.WriteLine("Parsing completed without garbage creation.");
	}
}