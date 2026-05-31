//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NubankVelocitySimulado
//{
//	public record Transaction(long TimeStamp, decimal Amount);

//	public class Solution
//	{
//		public string[] solution(string[][] queries)
//		{
//			var result = new List<string>();

//			var windowTransactionsPerUser = new Dictionary<string, List<Transaction>>();

//			foreach (var query in queries)
//			{
//				var timeStamp = long.Parse(query[0]);
//				var command = query[1];
//				var userId = query[2];
//				var amount = decimal.Parse(query[3]);

//				if (!windowTransactionsPerUser.ContainsKey(userId))
//				{
//					windowTransactionsPerUser.Add(userId, new List<Transaction> { new Transaction(timeStamp, amount) });
//				}

//				//remove expired transactions from sliding window
//				windowTransactionsPerUser[userId].RemoveAll(t => t.TimeStamp <= (timeStamp - 60));

//				//add trasaction to sliding window
//				var totalAmountInWindow = windowTransactionsPerUser[userId].Sum(t => t.Amount);

//				if (totalAmountInWindow > 1000)
//				{
//					result.Add("REJECTED");
//				}
//				else
//				{
//					windowTransactionsPerUser[userId].Add(new Transaction(timeStamp, amount));
//					result.Add("ACCEPTED");
//				}

//			}

//			return result.ToArray();
//		}
//	}

//	// --- CÓDIGO DO PROBLEMA (RUNNER) ---
//	class Program
//	{
//		static void Main(string[] args)
//		{
//			var sol = new Solution();

//			// [timestamp, command, userId, amount]
//			string[][] input = new string[][]
//			{
//				new string[] { "10", "TRANSFER", "user1", "500" },  // ACCEPTED (soma 500)
//                new string[] { "20", "TRANSFER", "user1", "400" },  // ACCEPTED (soma 900)
//                new string[] { "30", "TRANSFER", "user1", "200" },  // REJECTED (900+200 > 1000)
//                new string[] { "71", "TRANSFER", "user1", "600" },  // ACCEPTED (Tx de 10s expirou, soma era 400, 400+600 <= 1000)
//                new string[] { "72", "TRANSFER", "user2", "1100" }  // REJECTED (Acima do limite individual)
//			};

//			Console.WriteLine("=== Simulador de Monitoramento de Velocidade (Anti-Fraude) ===");
//			var results = sol.solution(input);

//			for (int i = 0; i < results.Length; i++)
//			{
//				Console.WriteLine($"Transação {i + 1}: {results[i]}");
//			}

//			/*
//			   Explicação do Caso de Teste:
//			   - T:10 -> user1 gasta 500. Total 60s: 500. (OK)
//			   - T:20 -> user1 gasta 400. Total 60s: 900. (OK)
//			   - T:30 -> user1 tenta 200. Total 60s seria 1100. (REJECTED)
//			   - T:71 -> A transação de T:10 saiu da janela (71-60 = 11). 
//						 Sobrou apenas a de T:20 (400). 
//						 400 + 600 = 1000. (OK)
//			*/

//			Console.WriteLine("\nPressione Enter para fechar.");
//			Console.ReadLine();
//		}
//	}
//}