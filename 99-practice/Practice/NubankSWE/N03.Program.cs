//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NubankSimuladoSenior
//{
//    // Estrutura para rastrear compras e permitir estornos
//    public record PurchaseRecord(string UserId, decimal Amount);

//    public class Solution
//    {
//        private class Commands
//        {
//            public const string SetLimit = "SET_LIMIT";
//            public const string Purchase = "PURCHASE";
//            public const string Refund = "REFUND";
//        }

//        public string[] solution(string[][] queries)
//        {
//            var totalLimits = new SortedDictionary<string, decimal>();
//            var usedLimits = new SortedDictionary<string, decimal>();
//            var purchases = new SortedDictionary<string, PurchaseRecord>();

//            foreach (var query in queries)
//            {
//                if (query.Length == 0)
//                {
//                    continue;
//                }

//                var command = query[0];

//                switch (command)
//                {
//                    case Commands.SetLimit:
//                        var limitUserId = query[1];
//                        var limitAmount = decimal.Parse(query[2]);
//                        totalLimits[limitUserId] = limitAmount;
//                        break;

//                    case Commands.Purchase:
//                        var purchaseUserId = query[1];
//                        var purchaseAmount = decimal.Parse(query[2]);
//                        var purchaseId = query[3];

//                        var totalLimit = totalLimits[purchaseUserId];

//                        if (!usedLimits.ContainsKey(purchaseUserId))
//                        {
//                            if (totalLimit < purchaseAmount)
//                                continue;
//                            usedLimits[purchaseUserId] = purchaseAmount;
//                        }
//                        else
//                        {
//                            if (totalLimit - usedLimits[purchaseUserId] < purchaseAmount)
//                                continue;
//                            usedLimits[purchaseUserId] += purchaseAmount;
//                        }
//                        purchases.Add(purchaseId, new PurchaseRecord(purchaseUserId, purchaseAmount));
//                        break;

//                    case Commands.Refund:
//                        var refundPurchaseId = query[1];
//                        if (purchases.ContainsKey(refundPurchaseId))
//                        {
//                            var purchase = purchases[refundPurchaseId];
//                            purchases.Remove(refundPurchaseId);
//                            if (usedLimits.ContainsKey(purchase.UserId))
//                            {
//                                usedLimits.Remove(purchase.UserId);
//                            }
//                        }
//                        break;

//                    default:
//                        break;
//                }
//            }

//            var result = new List<string>();

//            foreach (var totalLimit in totalLimits)
//            {
//                var userId = totalLimit.Key;
//                var usedLimit = 0m;
//                if (usedLimits.ContainsKey(userId))
//                    usedLimit = usedLimits[userId];
//                var limit = totalLimit.Value - usedLimit;
//                result.Add($"{userId}: {limit}");
//            }

//            Console.WriteLine("Limites disponíveis");
//            Console.WriteLine("===================");
//            return result.ToArray();
//        }
//    }

//    // --- CÓDIGO DO PROBLEMA (SIMULADOR DE EXECUÇÃO) ---
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var sol = new Solution();

//            string[][] input = new string[][]
//            {
//                new string[] { "SET_LIMIT", "alice", "1000" },
//                new string[] { "SET_LIMIT", "bob", "500" },
//                new string[] { "PURCHASE", "alice", "300", "p1" },
//                new string[] { "PURCHASE", "alice", "800", "p2" }, // Deve falhar (limite insuficiente)
//                new string[] { "PURCHASE", "bob", "100", "p3" },
//                new string[] { "REFUND", "p1" },                  // Alice volta a ter 1000
//                new string[] { "PURCHASE", "alice", "800", "p4" }, // Agora deve passar
//                new string[] { "REFUND", "p3" },                  // Bob volta a ter 500
//                new string[] { "REFUND", "p3" }                   // Tentativa de estorno duplo (deve ignorar)
//            };

//            Console.WriteLine("Executando Simulado de Cartão de Crédito...");
//            var results = sol.solution(input);

//            foreach (var r in results)
//            {
//                Console.WriteLine($"Disponível -> {r}");
//            }

//            /* 
//               Esperado:
//               alice:200 (1000 - 800 da p4)
//               bob:500   (500 - 100 + 100 do refund)
//            */

//            Console.WriteLine("\nTeste concluído. Verifique se os valores batem com a lógica.");
//            Console.ReadLine();
//        }
//    }
//}