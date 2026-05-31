//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NubankSimuladoSenior
//{
//    // Estrutura para rastrear compras e permitir estornos
//    public record PurchaseRecord(string UserId, decimal Amount);

//    public class Solution
//    {
//        public string[] solution(string[][] queries)
//        {
//            // Armazena o limite total configurado
//            var totalLimits = new Dictionary<string, decimal>();
//            // Armazena quanto do limite já foi utilizado
//            var usedBalances = new Dictionary<string, decimal>();
//            // Mapeia purchaseId para os dados da compra para permitir estorno
//            var activePurchases = new Dictionary<string, PurchaseRecord>();

//            foreach (var q in queries)
//            {
//                string command = q[0];

//                switch (command)
//                {
//                    case "SET_LIMIT":
//                        string userLimit = q[1];
//                        decimal limitAmt = decimal.Parse(q[2]);
//                        totalLimits[userLimit] = limitAmt;
//                        if (!usedBalances.ContainsKey(userLimit)) usedBalances[userLimit] = 0;
//                        break;

//                    case "PURCHASE":
//                        string userPur = q[1];
//                        decimal purAmt = decimal.Parse(q[2]);
//                        string purId = q[3];

//                        if (totalLimits.TryGetValue(userPur, out decimal limit))
//                        {
//                            decimal available = limit - usedBalances[userPur];
//                            if (purAmt <= available)
//                            {
//                                usedBalances[userPur] += purAmt;
//                                activePurchases[purId] = new PurchaseRecord(userPur, purAmt);
//                            }
//                        }
//                        break;

//                    case "REFUND":
//                        string refundId = q[1];
//                        if (activePurchases.TryGetValue(refundId, out var record))
//                        {
//                            // Devolve o valor ao limite disponível (diminuindo o uso)
//                            usedBalances[record.UserId] -= record.Amount;
//                            // Remove para evitar estorno duplo (Idempotência)
//                            activePurchases.Remove(refundId);
//                        }
//                        break;
//                }
//            }

//            // Gera o resultado: Usuários em ordem alfabética com o limite disponível
//            return totalLimits.Keys
//                .OrderBy(u => u)
//                .Select(u =>
//                {
//                    decimal available = totalLimits[u] - usedBalances[u];
//                    return $"{u}:{available}";
//                })
//                .ToArray();
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