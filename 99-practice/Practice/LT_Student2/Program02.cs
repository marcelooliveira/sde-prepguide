using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

//new solution take to exercise 2
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

		// TODO: Transfira os dados para uma coleção concorrente adequada do .NET Core 3.
		var newConfig = new ConcurrentDictionary<string, string>(rawConfig);

		string targetService = "ServiceB";
		// TODO: Realize a busca segura e ultra-rápida do targetService usando TryGetValue.
		if (newConfig.TryGetValue(targetService, out var value))
		{
			Console.WriteLine($"Value for config {targetService}: {value}");
		}
		else
		{
			Console.WriteLine($"No value found for config {targetService}");
		}

		Console.WriteLine("Search execution completed.");
	}
}