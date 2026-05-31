//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NubankVelocitySimulado
//{
//  // Representa uma transação bem-sucedida para controle de histórico
//  public record PastTx(long Timestamp, decimal Amount);

//  public class Solution
//  {
//    public string[] solution(string[][] queries)
//    {
//      // Dicionário para armazenar o histórico de transações aceitas por usuário
//      var userHistory = new Dictionary<string, List<PastTx>>();
//      List<string> results = new List<string>();

//      foreach (var q in queries)
//      {
//        long time = long.Parse(q[0]);
//        string userId = q[2];
//        decimal amount = decimal.Parse(q[3]);

//        if (!userHistory.ContainsKey(userId))
//        {
//          userHistory[userId] = new List<PastTx>();
//        }

//        // 1. Limpeza: Remove transações mais velhas que 60 segundos da janela atual
//        // Isso mantém a performance e a corretude do cálculo
//        userHistory[userId].RemoveAll(tx => tx.Timestamp <= time - 60);

//        // 2. Cálculo da "Velocidade": Soma do que foi gasto nos últimos 60s
//        decimal windowSum = userHistory[userId].Sum(tx => tx.Amount);

//        // 3. Regra de Negócio
//        if (windowSum + amount > 1000)
//        {
//          results.Add("REJECTED");
//        }
//        else
//        {
//          // Se aceita, adiciona ao histórico para impactar as próximas
//          userHistory[userId].Add(new PastTx(time, amount));
//          results.Add("ACCEPTED");
//        }
//      }

//      return results.ToArray();
//    }
//  }

//  // --- CÓDIGO DO PROBLEMA (RUNNER) ---
//  class Program
//  {
//    static void Main(string[] args)
//    {
//      var sol = new Solution();

//      // [timestamp, command, userId, amount]
//      string[][] input = new string[][]
//      {
//                new string[] { "10", "TRANSFER", "user1", "500" },  // ACCEPTED (soma 500)
//                new string[] { "20", "TRANSFER", "user1", "400" },  // ACCEPTED (soma 900)
//                new string[] { "30", "TRANSFER", "user1", "200" },  // REJECTED (900+200 > 1000)
//                new string[] { "71", "TRANSFER", "user1", "600" },  // ACCEPTED (Tx de 10s expirou, soma era 400, 400+600 <= 1000)
//                new string[] { "72", "TRANSFER", "user2", "1100" }  // REJECTED (Acima do limite individual)
//      };

//      Console.WriteLine("=== Simulador de Monitoramento de Velocidade (Anti-Fraude) ===");
//      var results = sol.solution(input);

//      for (int i = 0; i < results.Length; i++)
//      {
//        Console.WriteLine($"Transação {i + 1}: {results[i]}");
//      }

//      /*
//         Explicação do Caso de Teste:
//         - T:10 -> user1 gasta 500. Total 60s: 500. (OK)
//         - T:20 -> user1 gasta 400. Total 60s: 900. (OK)
//         - T:30 -> user1 tenta 200. Total 60s seria 1100. (REJECTED)
//         - T:71 -> A transação de T:10 saiu da janela (71-60 = 11). 
//                   Sobrou apenas a de T:20 (400). 
//                   400 + 600 = 1000. (OK)
//      */

//      Console.WriteLine("\nPressione Enter para fechar.");
//      Console.ReadLine();
//    }
//  }
//}