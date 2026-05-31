////Cenário 1: Processamento de Transações/Eventos com Foco em Resiliência e Negócio
////
////Implemente um método que processe uma lista de transações financeiras de PIX recebidas de um parceiro.
////Precisamos validar o saldo, garantir que transações duplicadas não sejam processadas (idempotência) e
////atualizar o saldo da conta de forma segura
//using System.Collections.Concurrent;

//namespace BtgLiveCodingDemo
//{
//	// 1. Entidades de Negócio Claras e Imutáveis
//	public record Transaction(string TransactionId, string AccountId, decimal Amount, DateTime Timestamp);

//	public class Account
//	{
//		public string AccountId { get; init; }
//		public decimal Balance { get; private set; }

//		// Uso de um dicionário thread-safe para garantir a idempotência (não reprocessar o mesmo ID)
//		private readonly ConcurrentDictionary<string, byte> _processedTransactions = new();

//		public Account(string accountId, decimal initialBalance)
//		{
//			AccountId = accountId;
//			Balance = initialBalance;
//		}

//		public bool TryProcessPix(Transaction transaction, out string errorMessage)
//		{
//			errorMessage = string.Empty;

//			// Validação de Idempotência: Se o ID já existe, ignora para evitar duplo débito
//			if (!_processedTransactions.TryAdd(transaction.TransactionId, 0))
//			{
//				errorMessage = $"Transação duplicada rejeitada: {transaction.TransactionId}";
//				return false;
//			}

//			// Validação de Regra de Negócio: Saldo insuficiente
//			if (Balance < transaction.Amount)
//			{
//				errorMessage = $"Saldo insuficiente na conta {AccountId}. Saldo: {Balance}, Tentativa: {transaction.Amount}";
//				return false;
//			}

//			// Atualização do Estado
//			Balance -= transaction.Amount;
//			return true;
//		}
//	}

//	// 2. O Processador (Onde a lógica do Live Coding acontece)
//	public class PixProcessor
//	{
//		public void ProcessBatch(List<Transaction> transactions, Dictionary<string, Account> accountsDb)
//		{
//			// Abordagem estruturada: validação rápida antes de começar
//			if (transactions == null || !transactions.Any())
//			{
//				Console.WriteLine("Lote de transações vazio. Nada a processar.");
//				return;
//			}

//			foreach (var tx in transactions)
//			{
//				// Tratamento de Resiliência: A conta existe na base?
//				if (!accountsDb.TryGetValue(tx.AccountId, out var account))
//				{
//					Console.WriteLine($"[ERRO CRÍTICO] Conta {tx.AccountId} não encontrada no banco de dados. Transação {tx.TransactionId} abortada.");
//					continue; // Pula para a próxima sem derrubar o sistema inteiro
//				}

//				// Execução da regra de negócio com captura de logs clara para sustentação
//				if (account.TryProcessPix(tx, out var error))
//				{
//					Console.WriteLine($"[SUCESSO] Transação {tx.TransactionId} processada. Novo saldo da conta {account.AccountId}: R${account.Balance}");
//				}
//				else
//				{
//					// Sinaliza o erro de negócio, mas mantém o processamento do lote vivo
//					Console.WriteLine($"[FALHA DE NEGÓCIO] {error}");
//				}
//			}
//		}
//	}

//	// 3. Ponto de Entrada para Demonstrar o Funcionamento
//	public class Program
//	{
//		public static void Main()
//		{
//			// Simulando nosso "Banco de Dados" em memória
//			var accountsDatabase = new Dictionary<string, Account>
//			{
//				{ "ACC-123", new Account("ACC-123", 1000.00m) }
//			};

//			var processor = new PixProcessor();

//			// Massa de teste contendo cenários de sucesso, saldo insuficiente e duplicidade
//			var txList = new List<Transaction>
//			{
//				new Transaction("TX-001", "ACC-123", 200.00m, DateTime.UtcNow),
//				new Transaction("TX-002", "ACC-123", 900.00m, DateTime.UtcNow), // Vai falhar por saldo
//                new Transaction("TX-001", "ACC-123", 200.00m, DateTime.UtcNow), // Duplicada (Mesmo ID)
//                new Transaction("TX-003", "ACC-999", 50.00m, DateTime.UtcNow)   // Conta inexistente
//            };

//			Console.WriteLine("Iniciando o processamento do lote BTG...");
//			processor.ProcessBatch(txList, accountsDatabase);
//		}
//	}
//}