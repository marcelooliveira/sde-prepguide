using System;

class Program10
{
	static void MainX()
	{
		// O entrevistador pode alterar este array para null, vazio, ou conter zeros/negativos a qualquer momento!
		int[] injectedData = { 4, 8, 15, 16, 23, 42 };

		try
		{
			double result = CalculateMetrics(injectedData);
			Console.WriteLine($"Metric Score: {result}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Engine gracefully protected against: {ex.Message}");
		}
	}

	static double CalculateMetrics(int[] data)
	{
		// TODO: Implemente as Cláusulas de Guarda de nível Sênior para evitar quebras de CPU (como Division by Zero).
		// Se os dados passarem, execute a lógica de agregação com segurança.

		double product = 1.0;
		foreach (var val in data)
		{
			product *= val;
		}
		return Math.Pow(product, 1.0 / data.Length);
	}
}