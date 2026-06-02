using System;
using System.Threading;
using System.Threading.Tasks;

//new solution take to exercise 8
class Program08
{
	// TODO: Instancie a primitiva correta do .NET para gerenciar acessos assíncronos concorrentes (limite de 2).

	private static SemaphoreSlim _semaphore = new SemaphoreSlim(2, 2);
	static async Task MainX()
	{
		Task[] calls = new Task[5];
		for (int i = 0; i < 5; i++)
		{
			int id = i;
			calls[i] = SimulateExternalCallAsync(id);
		}
		await Task.WhenAll(calls);
	}

	static async Task SimulateExternalCallAsync(int callId)
	{
		// TODO: Aguarde a liberação da trava de forma assíncrona.

		await _semaphore.WaitAsync();

		try
		{
			Console.WriteLine($"[ENTER] Call {callId} is executing inside the critical section...");
			await Task.Delay(1000); // Simulando operação I/O bound
			Console.WriteLine($"[EXIT] Call {callId} is leaving.");
		}
		finally
		{
			// TODO: Libere a trava de concorrência.
			_semaphore.Release();
		}

	}
}