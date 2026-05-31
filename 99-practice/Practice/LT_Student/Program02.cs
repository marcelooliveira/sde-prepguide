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

		// TODO: Transfira os dados para uma coleção concorrente adequada do .NET Core 3.

		string targetService = "ServiceB";
		// TODO: Realize a busca segura e ultra-rápida do targetService usando TryGetValue.

		Console.WriteLine("Search execution completed.");
	}
}