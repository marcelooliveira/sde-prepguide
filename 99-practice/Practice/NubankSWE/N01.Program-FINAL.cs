//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NubankSimuladoCodeSignal
//{
//    // --- ESTA É A CLASSE QUE VOCÊ PREENCHE NO CODESIGNAL ---
//    public class Solution
//    {
//        public string[] solution(string[][] queries)
//        {
//            // SortedDictionary mantém as chaves (UserIDs) ordenadas alfabeticamente
//            // Fundamental para passar nos testes de saída do CodeSignal sem esforço extra
//            var balances = new SortedDictionary<string, decimal>();

//            foreach (var q in queries)
//            {
//                if (q.Length == 0) continue;

//                string command = q[0];
//                string userId = q[1];

//                switch (command)
//                {
//                    case "DEPOSIT":
//                        decimal depAmount = decimal.Parse(q[2]);
//                        ExecuteDeposit(balances, userId, depAmount);
//                        break;

//                    case "WITHDRAW":
//                        decimal withAmount = decimal.Parse(q[2]);
//                        if (balances.ContainsKey(userId) && balances[userId] >= withAmount)
//                        {
//                            balances[userId] -= withAmount;
//                        }
//                        break;

//                    case "TRANSFER":
//                        string toId = q[2];
//                        decimal transAmount = decimal.Parse(q[3]);
//                        // Regra Sênior: Verifique se Origem != Destino e se há saldo
//                        if (userId != toId && balances.ContainsKey(userId) && balances[userId] >= transAmount)
//                        {
//                            balances[userId] -= transAmount;
//                            ExecuteDeposit(balances, toId, transAmount);
//                        }
//                        break;
//                }
//            }

//            // Converte o dicionário para o formato de saída esperado: ["user1:100", "user2:200"]
//            return balances.Select(x => $"{x.Key}:{x.Value}").ToArray();
//        }

//        private void ExecuteDeposit(SortedDictionary<string, decimal> balances, string id, decimal amount)
//        {
//            if (!balances.ContainsKey(id)) balances[id] = 0;
//            balances[id] += amount;
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