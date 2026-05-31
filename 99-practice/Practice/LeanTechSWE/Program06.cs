using System;

class Program06
{
	struct ServiceMetric
	{
		public int LatencyMs;
		public string Name;
	}

	static void MainX()
	{
		ServiceMetric[] metrics = new ServiceMetric[]
		{
			new ServiceMetric { LatencyMs = 150, Name = "Auth" },
			new ServiceMetric { LatencyMs = 30, Name = "Gateway" },
			new ServiceMetric { LatencyMs = 400, Name = "Payment" },
			new ServiceMetric { LatencyMs = 30, Name = "HealthCheck" }
		};

		// TODO: Ordene o array 'metrics' pelo campo LatencyMs de forma In-Place,
		// sem usar LINQ (.OrderBy), modificando diretamente a estrutura original.

		Console.WriteLine("In-place ordering completed.");
	}
}