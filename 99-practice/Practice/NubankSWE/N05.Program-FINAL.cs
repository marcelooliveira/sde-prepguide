//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NubankCloudStorageSimulado
//{
//	// Representa os metadados do arquivo
//	public record FileMetadata(string Id, int Size, long Expiry, int Priority, long CreatedAt);

//	public class Solution
//	{
//		private const int MAX_CAPACITY = 5000;
//		private Dictionary<string, FileMetadata> _storage = new();

//		public string[] solution(string[][] queries)
//		{
//			List<string> results = new List<string>();

//			foreach (var q in queries)
//			{
//				long currentTime = long.Parse(q[0]);
//				string command = q[1];

//				if (command == "ADD_FILE")
//				{
//					string id = q[2];
//					int size = int.Parse(q[3]);
//					int ttl = int.Parse(q[4]);
//					int priority = int.Parse(q[5]);

//					// 1. Limpeza automática de expirados antes de tentar adicionar
//					CleanupExpired(currentTime);

//					// 2. Verificação de capacidade e despejo (Eviction) por prioridade
//					while (GetCurrentSize() + size > MAX_CAPACITY && _storage.Any())
//					{
//						// Remove o de menor prioridade; se igual, o mais antigo (CreatedAt)
//						var toRemove = _storage.Values
//							.OrderBy(f => f.Priority)
//							.ThenBy(f => f.CreatedAt)
//							.First();

//						_storage.Remove(toRemove.Id);
//					}

//					// 3. Adiciona se couber (pode não caber se o arquivo sozinho for > 5000)
//					if (size <= MAX_CAPACITY)
//					{
//						_storage[id] = new FileMetadata(id, size, currentTime + ttl, priority, currentTime);
//						results.Add("SUCCESS");
//					}
//					else
//					{
//						results.Add("OVERFLOW");
//					}
//				}
//				else if (command == "GET_FILE")
//				{
//					string id = q[2];
//					CleanupExpired(currentTime); // Garante que não retorna arquivo expirado

//					if (_storage.TryGetValue(id, out var file))
//					{
//						results.Add(file.Id);
//					}
//					else
//					{
//						results.Add("NOT_FOUND");
//					}
//				}
//			}

//			return results.ToArray();
//		}

//		private void CleanupExpired(long currentTime)
//		{
//			var expiredKeys = _storage.Where(kvp => kvp.Value.Expiry <= currentTime)
//									  .Select(kvp => kvp.Key)
//									  .ToList();

//			foreach (var key in expiredKeys)
//			{
//				_storage.Remove(key);
//			}
//		}

//		private int GetCurrentSize() => _storage.Values.Sum(f => f.Size);
//	}

//	// --- CÓDIGO DO PROBLEMA (RUNNER) ---
//	class Program
//	{
//		static void Main(string[] args)
//		{
//			var sol = new Solution();

//			string[][] input = new string[][]
//			{
//                // [timestamp, command, id, size, ttl, priority]
//                new string[] { "10", "ADD_FILE", "file1", "3000", "100", "2" }, // OK
//                new string[] { "20", "ADD_FILE", "file2", "2500", "100", "1" }, // Estoura 5000. Expira nada. Remove file2 ou file1? File2 tem prioridade 1 (menor).
//                new string[] { "30", "GET_FILE", "file2" },                    // Deve ser NOT_FOUND
//                new string[] { "40", "ADD_FILE", "file3", "2500", "100", "3" }, // Estoura 5000. Remove file1 (prio 2) para caber file3 (prio 3).
//                new string[] { "150", "GET_FILE", "file3" }                    // NOT_FOUND (Expirou: 40 + 100 = 140)
//            };

//			Console.WriteLine("=== Simulador Cloud Storage (TTL & Priority) ===\n");
//			var results = sol.solution(input);

//			foreach (var r in results)
//			{
//				Console.WriteLine($"Resultado: {r}");
//			}

//			Console.WriteLine("\nClique em qualquer tecla para sair.");
//			Console.ReadKey();
//		}
//	}
//}