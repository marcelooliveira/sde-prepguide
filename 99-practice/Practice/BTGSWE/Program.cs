// Cenário 1: Processamento de Transações/Eventos com Foco em Resiliência e Negócio
//
// Implemente um método que processe uma lista de transações financeiras de PIX recebidas de um parceiro.
// Precisamos validar o saldo, garantir que transações duplicadas não sejam processadas (idempotência) e
// atualizar o saldo da conta de forma segura

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// Modelo de domínio: Representa uma transação PIX no negócio
record Transaction(string Id, string AccountId, decimal Amount);

// Modelo de domínio: Resultado do processamento com contexto de negócio
record ProcessingResult(bool Success, string Message, decimal? NewBalance = null);

class PixTransactionProcessor : IDisposable
{
    // Repositório em memória: Simula persistência de contas
    // Em produção: usar banco de dados com transações ACID
    private readonly Dictionary<string, decimal> _accounts = new();

    // Controle de idempotência: Garante que cada transação seja processada apenas uma vez
    // Termo de negócio: Evita duplicação de créditos/débitos na conta do cliente
    // Técnica: Cache de IDs processados (em produção: usar distributed cache como Redis)
    private readonly HashSet<string> _processedTransactions = new();

    // ⚠️ OPÇÃO 1 (LEGADO): Lock síncrono - Simples mas bloqueia threads
    // ❌ Não funciona com async/await
    // ❌ Bloqueia thread completamente (não é eficiente em alta concorrência)
    // ✅ Mais simples para casos síncronos
    private readonly object _lockObject = new();

    // ✅ OPÇÃO 2 (MODERNA - .NET 8): SemaphoreSlim - Assíncrono e eficiente
    // ✅ Funciona com async/await (não bloqueia threads desnecessariamente)
    // ✅ Suporta timeout e CancellationToken
    // ✅ Mais eficiente em cenários de alta concorrência (APIs, microservices)
    // Termo de negócio: Garante consistência do saldo durante processamento concorrente
    // Técnica: Semáforo com limite de 1 acesso simultâneo (mutual exclusion)
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public PixTransactionProcessor()
    {
        // SALDO INICIAL: conta "ACC-123" = 1000.00
        _accounts["ACC-123"] = 1000.00m;
    }

    // VERSÃO SÍNCRONA (LEGADO): Usando lock tradicional
    // Use quando: Aplicação console, batch jobs, processamento síncrono
    public ProcessingResult ProcessTransaction(Transaction transaction)
    {
        // TÉCNICA: Lock pessimista para garantir serialização das operações
        // NEGÓCIO: Previne inconsistências no saldo da conta
        lock (_lockObject)
        {
            // VALIDAÇÃO 1: Idempotência
            // NEGÓCIO: Cliente não pode receber/perder dinheiro duas vezes pela mesma transação
            // TÉCNICA: Verificação no cache de transações processadas
            if (_processedTransactions.Contains(transaction.Id))
            {
                return new ProcessingResult(
                    false, 
                    $"❌ Transação {transaction.Id} duplicada - Já processada anteriormente (Idempotência)"
                );
            }

            // VALIDAÇÃO 2: Existência da conta
            // NEGÓCIO: Conta deve existir no sistema para receber PIX
            // TÉCNICA: Lookup no repositório de contas
            if (!_accounts.ContainsKey(transaction.AccountId))
            {
                return new ProcessingResult(
                    false, 
                    $"❌ Conta {transaction.AccountId} não encontrada no sistema"
                );
            }

            // VALIDAÇÃO 3: Saldo suficiente (assumindo débito)
            // NEGÓCIO: Cliente não pode gastar mais do que tem (prevenção de saldo negativo)
            // TÉCNICA: Validação antes da atualização (optimistic validation)
            decimal currentBalance = _accounts[transaction.AccountId];
            decimal newBalance = currentBalance - transaction.Amount;

            if (newBalance < 0)
            {
                return new ProcessingResult(
                    false, 
                    $"❌ Saldo insuficiente na conta {transaction.AccountId}. " +
                    $"Saldo atual: R$ {currentBalance:F2}, Valor tentado: R$ {transaction.Amount:F2}"
                );
            }

            // PROCESSAMENTO: Atualização do saldo
            // NEGÓCIO: Débito efetivo da conta do cliente
            // TÉCNICA: Atualização atômica protegida pelo lock
            _accounts[transaction.AccountId] = newBalance;

            // REGISTRO: Marca transação como processada
            // NEGÓCIO: Auditoria e controle de idempotência
            // TÉCNICA: Inserção no cache de transações processadas
            _processedTransactions.Add(transaction.Id);

            return new ProcessingResult(
                true, 
                $"✅ Transação {transaction.Id} processada com sucesso",
                newBalance
            );
        }
    }

    // VERSÃO ASSÍNCRONA (MODERNA - .NET 8): Usando SemaphoreSlim
    // Use quando: APIs, microservices, aplicações web, alta concorrência
    // TÉCNICA: Async/await libera a thread durante wait (Thread Pool efficiency)
    // NEGÓCIO: Mesma garantia de consistência com melhor performance
    public async Task<ProcessingResult> ProcessTransactionAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        // TÉCNICA: Wait assíncrono com suporte a cancelamento
        // NEGÓCIO: Permite timeout em transações lentas (SLA de processamento)
        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            // VALIDAÇÃO 1: Idempotência
            if (_processedTransactions.Contains(transaction.Id))
            {
                return new ProcessingResult(
                    false, 
                    $"❌ Transação {transaction.Id} duplicada - Já processada anteriormente (Idempotência)"
                );
            }

            // VALIDAÇÃO 2: Existência da conta
            if (!_accounts.ContainsKey(transaction.AccountId))
            {
                return new ProcessingResult(
                    false, 
                    $"❌ Conta {transaction.AccountId} não encontrada no sistema"
                );
            }

            // VALIDAÇÃO 3: Saldo suficiente
            decimal currentBalance = _accounts[transaction.AccountId];
            decimal newBalance = currentBalance - transaction.Amount;

            if (newBalance < 0)
            {
                return new ProcessingResult(
                    false, 
                    $"❌ Saldo insuficiente na conta {transaction.AccountId}. " +
                    $"Saldo atual: R$ {currentBalance:F2}, Valor tentado: R$ {transaction.Amount:F2}"
                );
            }

            // PROCESSAMENTO: Atualização do saldo
            _accounts[transaction.AccountId] = newBalance;
            _processedTransactions.Add(transaction.Id);

            return new ProcessingResult(
                true, 
                $"✅ Transação {transaction.Id} processada com sucesso",
                newBalance
            );
        }
        finally
        {
            // CRÍTICO: Release deve sempre executar, mesmo em caso de exception
            // TÉCNICA: Try-finally garante liberação do semáforo (evita deadlock)
            // NEGÓCIO: Garante que próximas transações possam ser processadas
            _semaphore.Release();
        }
    }

    // Método auxiliar: Consulta saldo atual
    public decimal? GetBalance(string accountId)
    {
        return _accounts.TryGetValue(accountId, out var balance) ? balance : null;
    }

    // Cleanup: Libera recursos do SemaphoreSlim
    public void Dispose()
    {
        _semaphore?.Dispose();
    }
}

class Program
{
    static async Task Main()
    {
        var processor = new PixTransactionProcessor();

        // CENÁRIO DE TESTE: Lista de transações recebidas do parceiro
        var transactions = new List<Transaction>
        {
            new("TX-001", "ACC-123", 200.00m),  // ✅ OK - Saldo suficiente
            new("TX-002", "ACC-123", 900.00m),  // ❌ Vai falhar por saldo insuficiente
            new("TX-001", "ACC-123", 200.00m),  // ❌ Duplicada (Mesmo ID) - Idempotência
            new("TX-003", "ACC-999", 50.00m)    // ❌ Conta inexistente
        };

        Console.WriteLine("=== PROCESSAMENTO DE TRANSAÇÕES PIX ===\n");
        Console.WriteLine($"Saldo inicial ACC-123: R$ {processor.GetBalance("ACC-123"):F2}\n");

        // Processamento em lote com resiliência
        // NEGÓCIO: Continua processando mesmo se uma transação falhar
        // TÉCNICA: Exception handling individual por transação
        foreach (var transaction in transactions)
        {
            Console.WriteLine($"Processando: {transaction.Id} | Conta: {transaction.AccountId} | Valor: R$ {transaction.Amount:F2}");

            var result = processor.ProcessTransaction(transaction);
            Console.WriteLine($"  {result.Message}");

            if (result.Success && result.NewBalance.HasValue)
            {
                Console.WriteLine($"  Novo saldo: R$ {result.NewBalance.Value:F2}");
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Saldo final ACC-123: R$ {processor.GetBalance("ACC-123"):F2}");

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("=== VERSÃO MODERNA ASYNC (RECOMENDADA PARA .NET 8) ===");
        Console.WriteLine(new string('=', 60) + "\n");

        // Reset para demonstração da versão async
        var asyncProcessor = new PixTransactionProcessor();

        Console.WriteLine($"Saldo inicial ACC-123: R$ {asyncProcessor.GetBalance("ACC-123"):F2}\n");

        // Processamento assíncrono paralelo (simula API requests concorrentes)
        // TÉCNICA: Task.WhenAll para processar múltiplas transações em paralelo
        // NEGÓCIO: Maior throughput sem comprometer consistência
        var tasks = transactions.Select(async transaction =>
        {
            Console.WriteLine($"[ASYNC] Processando: {transaction.Id} | Conta: {transaction.AccountId} | Valor: R$ {transaction.Amount:F2}");

            // Simula chamada com timeout de 5 segundos (SLA)
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var result = await asyncProcessor.ProcessTransactionAsync(transaction, cts.Token);

            Console.WriteLine($"[ASYNC]   {result.Message}");
            if (result.Success && result.NewBalance.HasValue)
            {
                Console.WriteLine($"[ASYNC]   Novo saldo: R$ {result.NewBalance.Value:F2}");
            }
            Console.WriteLine();

            return result;
        });

        await Task.WhenAll(tasks);

        Console.WriteLine($"Saldo final ACC-123: R$ {asyncProcessor.GetBalance("ACC-123"):F2}");

        asyncProcessor.Dispose();
        processor.Dispose();

        Console.WriteLine("\n💡 COMPARAÇÃO:");
        Console.WriteLine("   • lock (sync)       → Simples, mas bloqueia threads");
        Console.WriteLine("   • SemaphoreSlim     → Moderno, async/await, mais eficiente");
        Console.WriteLine("   • Para .NET 8 APIs  → Sempre prefira SemaphoreSlim!");
    }
}

