using System;

//new solution take to the exercise 6
public struct ServiceMetric : IComparable<ServiceMetric>
{
	public int LatencyMs;
	public string Name;

	public int CompareTo(ServiceMetric other)
	{
		if (LatencyMs < other.LatencyMs)
			return -1;
		else if (LatencyMs > other.LatencyMs)
			return 1;
		else
			return 0;
	}
}

class Program06
{

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

		Array.Sort(metrics);

		Console.WriteLine("In-place ordering completed.");
	}
}