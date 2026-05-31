using System;

class Program10
{
	static void MainX()
	{
		// ATENÇÃO: O entrevistador poderá alterar este array para null, para um array vazio {}, 
		// ou introduzir um elemento 0 no meio da execução para quebrar seu código!
		int[] inputData = { 10, 20, 30, 40 };

		try
		{
			double executionResult = ComputeWeightedMetrics(inputData);
			Console.WriteLine($"Result: {executionResult}");
		}
		catch (ArgumentException ex)
		{
			Console.WriteLine($"Engine gracefully caught a business exception: {ex.Message}");
		}
	}

	static double ComputeWeightedMetrics(int[] data)
	{
		// TODO: Implemente aqui as validações de nível Sênior para interceptar problemas de dados
		// antes que eles causem falhas fatais ou exceções não tratadas na CPU (ex: DivideByZeroException).

		double totalSum = 0;
		foreach (var item in data)
		{
			totalSum += item;
		}

		// Exemplo de ponto crítico: se a soma for zero, gerará uma falha matemática
		return 1000.0 / totalSum;
	}
}