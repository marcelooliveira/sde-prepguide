using System;
using System.Collections.Generic;
using System.Collections.Immutable;

class Program02
{
	static void MainX()
	{
		// Configurações estáticas vindas de um banco/arquivo
		var rawConfig = new Dictionary<string, string>
		{
			{ "ServiceA", "http://api.servicea.internal" },
			{ "ServiceB", "http://api.serviceb.internal" },
			{ "ServiceC", "http://api.servicec.internal" }
		};

		// TODO: Transforme a coleção acima em uma estrutura otimizada do .NET 8 
		// para cenários de altíssima concorrência e leitura imutável (Frozen).

		string targetService = "ServiceB";
		// TODO: Realize a busca ultra-rápida do targetService.

		Console.WriteLine($"Route found for {targetService}");
	}
}