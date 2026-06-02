using System;

// new solution take for the exercise 3
class Program03
{
	static void MainX()
	{
		string rawPayload = "LOG|2026-05-31|TX_99887766|SUCCESS";

		// TODO: Use ReadOnlySpan<char> e os métodos .AsSpan(), .IndexOf() e .Slice()
		// para isolar o trecho "TX_99887766" com custo zero de alocação de memória.

		var newPayload = rawPayload.AsSpan();
		var targetColIndex = 2;
		var left = 0;
		var nextPipeIndex = newPayload.IndexOf("|");
		var right = nextPipeIndex == -1 ? newPayload.Length : nextPipeIndex;

		for (int i = 0; i < targetColIndex; i++)
		{
			left = right + 1;
			newPayload = newPayload.Slice(left, newPayload.Length - left - 1);
			nextPipeIndex = newPayload.IndexOf("|");
			right = nextPipeIndex == -1 ? newPayload.Length : nextPipeIndex;
		}

		var segment = newPayload.Slice(0, right);

		Console.WriteLine($"Segment found for column {targetColIndex}: {segment}");

		Console.WriteLine("Parsing completed without garbage creation.");
	}
}