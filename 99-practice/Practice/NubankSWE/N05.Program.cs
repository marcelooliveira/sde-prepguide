//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;

//namespace NubankCloudStorageSimulado
//{
//	public record File(int Timestamp, string FileId, int Size, int Ttl, int Priority);
//	public record GetFileCommand(int Timestamp, string FileId);

//	public class Solution
//	{
//		const int MaxMemorySize = 5000;
//		int usedMemory = 0;
//		SortedDictionary<string, File> files = new SortedDictionary<string, File>();
//		int currentTimestamp = 0;

//		public string[] solution(string[][] queries)
//		{
//			/*
//			**Objetivo:**
//			Implementar um gerenciador de arquivos em memória. Cada arquivo ocupa um espaço e tem um "tempo de vida".
//			Se o armazenamento atingir o limite, você deve remover os arquivos expirados. 
//			Se ainda assim não houver espaço, deve-se remover os arquivos de **menor prioridade**.

//			**Comandos:**
//			1.  **ADD_FILE `<timestamp>` `<fileId>` `<size>` `<ttl>` `<priority>`**: Adiciona um arquivo.
//				*   `<ttl>` é o tempo que o arquivo dura a partir do `<timestamp>`.
//				*   Se o espaço total exceder **5000**, o sistema deve:
//					1. Remover todos os arquivos cujo `timestamp + ttl <= currentTimestamp`.
//					2. Se ainda não couber, remover os arquivos de **menor prioridade**
//						(em caso de empate na prioridade, remover o mais antigo).
//			2.  **GET_FILE `<timestamp>` `<fileId>`**: Retorna o `fileId` se ele ainda existir e não tiver expirado.
//						Caso contrário, retorna `"NOT_FOUND"`.
//			*/

//			var result = new List<string>();

//			foreach (var query in queries)
//			{
//				var command = query[1];
//				switch (command)
//				{
//					case "ADD_FILE":
//						var file = new File(int.Parse(query[0]), query[2], int.Parse(query[3]), int.Parse(query[4]), int.Parse(query[5]));
//						currentTimestamp = file.Timestamp;

//						var usedMemory = files.Sum(f => f.Value.Size);
//						while (usedMemory + file.Size >= MaxMemorySize)
//						{
//							usedMemory = files.Sum(f => f.Value.Size);

//							var expiredFileIds = files.Where(f => f.Value.Timestamp + f.Value.Ttl <= currentTimestamp).Select(f => f.Key);
//							foreach (var item in expiredFileIds)
//							{
//								files.Remove(item);
//							}

//							var leastPriorityFileId = files.OrderBy(f => f.Value.Priority).Select(f => f.Key).FirstOrDefault();
//							if (leastPriorityFileId != null)
//								files.Remove(leastPriorityFileId);

//							usedMemory = files.Sum(f => f.Value.Size);
//						}

//						files.Add(file.FileId, file);
//						usedMemory += file.Size;
//						Console.WriteLine(file.ToString());
//						Console.WriteLine($"usedMemory: {usedMemory}");
//						break;
//					case "GET_FILE":
//						var getFileCommand = new GetFileCommand(int.Parse(query[0]), query[2]);
//						currentTimestamp = getFileCommand.Timestamp;
//						if (files.ContainsKey(getFileCommand.FileId))
//						{
//							result.Add(getFileCommand.FileId);
//						}
//						else
//						{
//							result.Add("NOT_FOUND");
//						}
//						break;
//					default:
//						break;
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