using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program02
{
	static void MainX()
	{
		var rawConfig = new Dictionary<string, string>
		{
			{ "ServiceA", "http://api.servicea.internal" },
			{ "ServiceB", "http://api.serviceb.internal" },
			{ "ServiceC", "http://api.servicec.internal" }
		};

		//Solution to the Exercise 2

		// TODO: Transfira os dados para uma coleção concorrente adequada do .NET Core 3.

		var newConfig = new ConcurrentDictionary<string, string>(rawConfig);

		string targetService = "ServiceB";
		// TODO: Realize a busca segura e ultra-rápida do targetService usando TryGetValue.

		if (newConfig.TryGetValue(targetService, out string? value)) //defensive search
		{
			Console.WriteLine($"Value: {value}");
		}
		else
		{
			Console.WriteLine($"Value not found for service: {targetService}");
		}

		Console.WriteLine("Search execution completed.");
	}
}