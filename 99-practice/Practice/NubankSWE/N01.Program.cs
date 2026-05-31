//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NubankSimuladoCodeSignal
//{
//    // --- ESTA É A CLASSE QUE VOCÊ PREENCHE NO CODESIGNAL ---

//    public class Solution
//    {
//        private class Operation
//        {
//            public const string Deposit = "DEPOSIT";
//            public const string Transfer = "TRANSFER";
//            public const string Withdraw = "WITHDRAW";
//        }

//        public string[] solution(string[][] queries)
//        {
//            var balances = new SortedDictionary<string, decimal>();

//            foreach (var query in queries)
//            {
//                if (query.Length == 0)
//                    continue;

//                var operation = query[0];
//                var userId = query[1];
//                switch (operation)
//                {
//                    case Operation.Deposit:
//                        var depositValue = decimal.Parse(query[2]);
//                        ExecuteDeposit(balances, userId, depositValue);
//                        break;
//                    case Operation.Transfer:
//                        var targetUser = query[2];
//                        var transferValue = decimal.Parse(query[3]);
//                        if (balances[userId] < transferValue)
//                            continue;

//                        ExecuteDeposit(balances, targetUser, transferValue);
//                        balances[userId] -= transferValue;
//                        break;
//                    case Operation.Withdraw:
//                        var withdrawValue = decimal.Parse(query[2]);
//                        if (!balances.ContainsKey(userId) ||  balances[userId] < withdrawValue)
//                            continue;
//                        balances[userId] -= withdrawValue;
//                        break;
//                }                    
//            }

//            return balances.Select(b => $"{b.Key}:{b.Value}").ToArray();
//        }

//        private static void ExecuteDeposit(SortedDictionary<string, decimal> balances, string userId, decimal depositValue)
//        {
//            if (!balances.ContainsKey(userId))
//                balances[userId] = depositValue; //O(log n)
//            else
//                balances[userId] += depositValue; //O(log n)
//        }
//    }

//    // --- ESTE É O "RUNNER" (O código que executa o problema para você testar) ---
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var sol = new Solution();

//            // Simulação de Input do CodeSignal (Array de Arrays de Strings)
//            string[][] testQueries = new string[][]
//            {
//                new string[] { "DEPOSIT", "marcelo", "1000" },
//                new string[] { "DEPOSIT", "luciana", "500" },
//                new string[] { "TRANSFER", "marcelo", "luciana", "200" },
//                new string[] { "WITHDRAW", "marcelo", "100" },
//                new string[] { "TRANSFER", "luciana", "kaue", "150" }, // kaue não existia, será criado
//                new string[] { "WITHDRAW", "marcelo", "2000" }         // Deve ser ignorado (saldo insuficiente)
//            };

//            Console.WriteLine("=== Iniciando Simulado CodeSignal - Nubank ===\n");

//            string[] result = sol.solution(testQueries);

//            Console.WriteLine("Resultado Final (Saldos Ordenados):");
//            foreach (var line in result)
//            {
//                Console.WriteLine(line);
//            }

//            // Validação simples
//            // Marcelo: 1000 - 200 (transf) - 100 (withdraw) = 700
//            // Luciana: 500 + 200 (transf) - 150 (transf) = 550
//            // Kaue: 150

//            Console.WriteLine("\nVerificação Manual:");
//            Console.WriteLine("Esperado: kaue:150, luciana:550, marcelo:700");

//            Console.WriteLine("\nPressione qualquer tecla para encerrar...");
//            Console.ReadKey();
//        }
//    }
//}