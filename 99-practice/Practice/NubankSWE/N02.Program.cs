//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.CompilerServices;

//namespace NubankCodeSignalAdvanced
//{
//    /*
//      Neste cenário, não basta processar comandos; você precisa lidar com Persistência Temporal (Timestamps) 
//      e Agendamento de Eventos (Cashback Pendente).

//      O Problema: "Advanced Banking Ledger with Delayed Cashback"
//      Enunciado: Implemente um sistema que processa operações (DEPOSIT, TRANSFER, WITHDRAW).

//      1. Timestamp: Cada operação tem um tempo 
//      2. Cashback: Cada DEPOSIT gera um bônus de 2% que só é liberado 24 horas (86.400 segundos) após o 
//      depósito, se a conta ainda estiver ativa.
//      3. Consulta de Saldo: O comando GET_BALANCE <time> <userId> deve retornar o saldo disponível no tempo t,
//      incluindo cashbacks que já "venceram" (passaram 24h).
//    */

//    public record BonusSchedule(long dueTime, decimal bonus);

//    public class Solution
//    {
//        public string[] solution(string[][] queries)
//        {
//            var balances = new SortedDictionary<string, decimal>();
//            var cashbacks = new Dictionary<string, List<BonusSchedule>>();

//            foreach (var query in queries)
//            {
//                if (query.Length == 0)
//                    continue;

//                var currentTime = long.Parse(query[0]);
//                var command = query[1];
//                var userId = query[2];

//                switch (command)
//                {
//                    case "DEPOSIT":
//                        var depAmount = decimal.Parse(query[3]);
//                        RedeemBonuses(cashbacks, balances, currentTime, userId);
//                        ExecuteDeposit(balances, userId, depAmount);
//                        ScheduleBonus(cashbacks, currentTime, userId, depAmount);
//                        break;
//                    case "TRANSFER":
//                        var toUserId = query[3];
//                        var transferAmount = decimal.Parse(query[4]);
//                        RedeemBonuses(cashbacks, balances, currentTime, userId);
//                        if (balances.ContainsKey(userId) && balances[userId] >= transferAmount)
//                        {
//                            balances[userId] -= transferAmount;
//                            ExecuteDeposit(balances, toUserId, transferAmount);
//                        }

//                        break;
//                    case "GET_BALANCE":
//                        if (balances.ContainsKey(userId))
//                        {
//                            Console.WriteLine($"Time: {currentTime}, User: {userId}, Balance: {balances[userId]}");
//                        }

//                        RedeemBonuses(cashbacks, balances, currentTime, userId);
//                        if (cashbacks.ContainsKey(userId))
//                        {
//                            var expiredBonuses = cashbacks[userId].Where(cb => cb.dueTime < currentTime);
//                            foreach (var expiredBonus in expiredBonuses)
//                            {
//                                Console.WriteLine($"UserId: {userId}, Due Time: {expiredBonus.dueTime}, Expired bonus: {expiredBonus.bonus}");
//                            }
//                        }
//                        break;
//                }
//            }

//            return balances.Select(b => $"{b.Key}:{b.Value}").ToArray();
//        }

//        private static void ScheduleBonus(Dictionary<string, List<BonusSchedule>> cashbacks, long currentTime, string userId, decimal depAmount)
//        {
//            List<BonusSchedule> bonusSchedules;
//            if (cashbacks.ContainsKey(userId))
//            {
//                bonusSchedules = cashbacks[userId];
//            }
//            else
//            {
//                bonusSchedules = new List<BonusSchedule>();
//            }
//            var bonus = depAmount * .02m;
//            bonusSchedules.Add(new BonusSchedule(currentTime + 86400, bonus));
//            cashbacks[userId] = bonusSchedules;
//        }

//        private static void RedeemBonuses(Dictionary<string, List<BonusSchedule>> cashbacks, SortedDictionary<string, decimal> balances, long currentTime, string userId)
//        {
//            if (cashbacks.ContainsKey(userId))
//            {
//                var expiredBonusAmount = cashbacks[userId].Where(cb => cb.dueTime < currentTime).Sum(cb => cb.bonus);
//                cashbacks[userId].RemoveAll(cb => cb.dueTime < currentTime);
//                if (expiredBonusAmount > 0)
//                {
//                    ExecuteDeposit(balances, userId, expiredBonusAmount);
//                }
//            }
//        }

//        private static void ExecuteDeposit(SortedDictionary<string, decimal> balances, string userId, decimal depAmount)
//        {
//            if (!balances.ContainsKey(userId))
//            {
//                balances[userId] = depAmount;
//            }
//            else
//            {
//                balances[userId] += depAmount;
//            }
//        }
//    }

//    // --- CÓDIGO DO PROBLEMA (RUNNER) ---
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var sol = new Solution();

//            // Formato CodeSignal: [timestamp, command, arg1, arg2, ...]
//            string[][] testQueries = new string[][]
//            {
//                // Marcelo deposita 1000 no tempo 0
//                new string[] { "0", "DEPOSIT", "marcelo", "1000" }, 
//                // Luciana deposita 500 no tempo 100
//                new string[] { "100", "DEPOSIT", "luciana", "500" },
//                // Verifica saldo no tempo 500 (Cashback ainda não caiu)
//                new string[] { "500", "GET_BALANCE", "marcelo" },
//                // Transfere 200 de marcelo para luciana no tempo 1000
//                new string[] { "1000", "TRANSFER", "marcelo", "luciana", "200" },
//                // Marcelo consulta saldo no tempo 86401 (O cashback de 20 reais deve ter caído)
//                new string[] { "86401", "GET_BALANCE", "marcelo" },
//                // Luciana consulta saldo no tempo 86501 (O cashback de 10 reais deve ter caído)
//                new string[] { "86501", "GET_BALANCE", "luciana" }
//            };

//            Console.WriteLine("=== Simulador Nubank Advanced (Cashback & Time) ===\n");

//            var results = sol.solution(testQueries);

//            foreach (var r in results)
//            {
//                Console.WriteLine($"Output: {r}");
//            }

//            Console.WriteLine("\n--- Análise do Resultado ---");
//            Console.WriteLine("Esperado Marcelo 86401: 1000 - 200 + 20 (cashback) = 820");
//            Console.WriteLine("Esperado Luciana 86501: 500 + 200 + 10 (cashback) = 710");

//            Console.ReadKey();
//        }
//    }
//}
