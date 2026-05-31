using System;
using System.Threading;
using System.Threading.Tasks;

class Program08
{
	// TODO: Instancie o mecanismo de sincronização assíncrona correto para limitar a 3 acessos.

	static async Task MainX()
	{
		Task[] users = new Task[10];
		for (int i = 0; i < 10; i++)
		{
			int userId = i;
			users[i] = ProcessPaymentAsync(userId);
		}
		await Task.WhenAll(users);
	}

	static async Task ProcessPaymentAsync(int userId)
	{
		// TODO: Adquira a trava de concorrência aqui de forma não bloqueante.

		Console.WriteLine($"[ENTER] User {userId} processing payment...");
		await Task.Delay(500); // Simulando I/O-bound
		Console.WriteLine($"[EXIT] User {userId} finished.");

		// TODO: Libere a trava.
	}
}