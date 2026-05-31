using System;

class Program03
{
	static void MainX()
	{
		string rawPayload = "LOG|2026-05-31|TX_99887766|SUCCESS";

		// TODO: Use ReadOnlySpan<char> e os métodos .AsSpan(), .IndexOf() e .Slice()
		// para isolar o trecho "TX_99887766" com custo zero de alocação de memória.

		Console.WriteLine("Parsing completed without garbage creation.");
	}
}