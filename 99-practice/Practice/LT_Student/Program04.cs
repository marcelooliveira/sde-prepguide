using System;
using System.Buffers;

class Program04
{
	static void MainX()
	{
		int requiredSize = 65536; // 64KB

		// TODO: Use o ArrayPool<byte>.Shared para alugar um buffer temporário,
		// use-o e garanta a sua devolução segura dentro de um bloco try/finally.
		var pool = ArrayPool<byte>.Shared.Rent(requiredSize);

		Console.WriteLine("Array leased, utilized, and returned safely to the pool.");
	}
}