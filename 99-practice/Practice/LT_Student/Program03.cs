using System;

class Program03
{
	static void MainX()
	{
		string rawPayload = "LOG|2026-05-31|TX_99887766|SUCCESS";

		// TODO: Use Span<char> ou ReadOnlySpan<char> para fatiar (slice) a string 
		// e isolar o trecho "TX_99887766" sem alocar uma nova string na memória (Heap).

		Console.WriteLine("Extraction completed. Check memory profile.");
	}
}