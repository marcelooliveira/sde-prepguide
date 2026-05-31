//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NubankCodeSignalAdvanced
//{
//    // Estrutura para controlar cashbacks que serão liberados no futuro
//    public record PendingBonus(long ReleaseTime, decimal Amount);

//    public class Solution
//    {
//        private readonly SortedDictionary<string, decimal> _balances = new();
//        private readonly Dictionary<string, List<PendingBonus>> _bonuses = new();

//        public string[] solution(string[][] queries)
//        {
//            List<string> results = new List<string>();

//            foreach (var q in queries)
//            {
//                long currentTime = long.Parse(q[0]);
//                string command = q[1];

//                if (command == "DEPOSIT")
//                {
//                    string userId = q[2];
//                    decimal amount = decimal.Parse(q[3]);

//                    ProcessExpiredBonuses(userId, currentTime);
//                    Deposit(userId, amount);

//                    // Agenda cashback de 2% para T + 86400
//                    ScheduleCashback(userId, amount, currentTime);
//                }
//                else if (command == "TRANSFER")
//                {
//                    string fromId = q[2];
//                    string toId = q[3];
//                    decimal amount = decimal.Parse(q[4]);

//                    ProcessExpiredBonuses(fromId, currentTime);
//                    ProcessExpiredBonuses(toId, currentTime);

//                    if (_balances.ContainsKey(fromId) && _balances[fromId] >= amount)
//                    {
//                        _balances[fromId] -= amount;
//                        Deposit(toId, amount);
//                    }
//                }
//                else if (command == "GET_BALANCE")
//                {
//                    string userId = q[2];
//                    ProcessExpiredBonuses(userId, currentTime);

//                    decimal currentBalance = _balances.GetValueOrDefault(userId, 0);
//                    results.Add($"{userId}:{currentBalance}");
//                }
//            }

//            return results.ToArray();
//        }

//        private void Deposit(string id, decimal amount)
//        {
//            if (!_balances.ContainsKey(id)) _balances[id] = 0;
//            _balances[id] += amount;
//        }

//        private void ScheduleCashback(string id, decimal amount, long time)
//        {
//            if (!_bonuses.ContainsKey(id)) _bonuses[id] = new List<PendingBonus>();
//            _bonuses[id].Add(new PendingBonus(time + 86400, amount * 0.02m));
//        }

//        private void ProcessExpiredBonuses(string id, long currentTime)
//        {
//            if (_bonuses.ContainsKey(id))
//            {
//                var expired = _bonuses[id].Where(b => b.ReleaseTime <= currentTime).ToList();
//                foreach (var b in expired)
//                {
//                    _balances[id] = _balances.GetValueOrDefault(id, 0) + b.Amount;
//                }
//                // Remove os bônus que já foram creditados
//                _bonuses[id].RemoveAll(b => b.ReleaseTime <= currentTime);
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
