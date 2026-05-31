using System;

class Program06
{
	struct PerformanceMetrics
	{
		public int ExecutionTime { get; set; }
		public string ServiceName { get; set; }
	}

	static void MainX()
	{
		PerformanceMetrics[] metrics = new PerformanceMetrics[]
		{
			new() { ExecutionTime = 120, ServiceName = "Auth" },
			new() { ExecutionTime = 45, ServiceName = "Gateway" },
			new() { ExecutionTime = 300, ServiceName = "Payment" },
			new() { ExecutionTime = 45, ServiceName = "Logging" }
		};

		// TODO: Ordene o array "metrics" com base no ExecutionTime.
		// Requisito: Deve ser ordenado In-Place (modificando o próprio array sem criar cópias).

		Console.WriteLine("In-place sort executed successfully.");
	}
}