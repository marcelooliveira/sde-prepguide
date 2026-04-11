# Heaps — Min-Heap, Max-Heap e Aplicações

Heaps são estruturas de dados usadas para acessar o menor ou maior elemento em O(log n). Essenciais para o problema "Running Median" do guia.

---

## Conceito

Um **heap** é uma árvore binária completa com propriedade de ordem:
- **Min-heap:** o menor elemento está sempre na raiz
- **Max-heap:** o maior elemento está sempre na raiz

Em C#, use `PriorityQueue<TElement, TPriority>` (disponível no .NET 6+, suportado pelo HackerRank). Por padrão é um **min-heap** — menor prioridade sai primeiro.

```
Min-heap:
        1
       / \
      3   5
     / \   \
    7   4   8
```

---

## `PriorityQueue<TElement, TPriority>` em C#

```csharp
// Min-heap: menor prioridade sai primeiro
var heap = new PriorityQueue<int, int>();
heap.Enqueue(5, 5); // (elemento, prioridade)
heap.Enqueue(1, 1);
heap.Enqueue(3, 3);

int minVal = heap.Peek();     // peek: 1 (sem remover)
int popped = heap.Dequeue();  // pop: 1

// Para inicializar com vários valores:
var nums = new[] { 5, 3, 8, 1, 4 };
var pq = new PriorityQueue<int, int>();
foreach (int n in nums) pq.Enqueue(n, n);
```

### Complexidades:
| Operação | Complexidade |
|---|---|
| Enqueue / Dequeue | O(log n) |
| Peek | O(1) |
| Enqueue n itens | O(n log n) |

---

## Max-Heap (negar a prioridade)

```csharp
var maxHeap = new PriorityQueue<int, int>();
maxHeap.Enqueue(5, -5); // negar prioridade = max-heap
maxHeap.Enqueue(1, -1);
maxHeap.Enqueue(3, -3);

int maxVal = maxHeap.Dequeue(); // 5
```

---

## Problema Clássico: Running Median (do guia)

> Dado um stream de números, retorne a mediana após cada inserção.

**Estratégia:** dois heaps
- **Max-heap (left):** metade menor dos números (armazenamos negativos)
- **Min-heap (right):** metade maior dos números

Invariante: `len(left) == len(right)` ou `len(left) == len(right) + 1`

```csharp
double[] RunningMedian(int[] stream)
{
    // maxHeap: metade esquerda — negar prioridade para max-heap
    var maxHeap = new PriorityQueue<int, int>();
    // minHeap: metade direita — min-heap normal
    var minHeap = new PriorityQueue<int, int>();
    var medians = new List<double>();

    foreach (int num in stream)
    {
        // 1. Inserir no heap correto
        if (maxHeap.Count == 0 || num <= maxHeap.Peek())
            maxHeap.Enqueue(num, -num); // negar para max-heap
        else
            minHeap.Enqueue(num, num);

        // 2. Rebalancear os heaps
        if (maxHeap.Count > minHeap.Count + 1)
        {
            int val = maxHeap.Dequeue();
            minHeap.Enqueue(val, val);
        }
        else if (minHeap.Count > maxHeap.Count)
        {
            int val = minHeap.Dequeue();
            maxHeap.Enqueue(val, -val);
        }

        // 3. Calcular mediana
        double median = (maxHeap.Count == minHeap.Count)
            ? (maxHeap.Peek() + minHeap.Peek()) / 2.0
            : maxHeap.Peek();

        medians.Add(median);
    }
    return medians.ToArray();
}
// RunningMedian([12, 4, 5, 3, 8, 7]) → [12.0, 8.0, 5.0, 4.5, 5.0, 6.0]
```

**Complexidade:** O(n log n) — cada operação de heap é O(log n)

---

## K-th Largest Element

```csharp
int KthLargest(int[] nums, int k)
{
    // Mantém um min-heap de tamanho k
    var heap = new PriorityQueue<int, int>();
    foreach (int num in nums)
    {
        heap.Enqueue(num, num);
        if (heap.Count > k) heap.Dequeue();
    }
    return heap.Peek(); // menor do heap = k-ésimo maior geral
}
// KthLargest([3, 2, 1, 5, 6, 4], 2) → 5
```

---

## K Elementos Mais Frequentes

```csharp
int[] TopKFrequent(int[] nums, int k)
{
    var count = new Dictionary<int, int>();
    foreach (int n in nums)
        count[n] = count.GetValueOrDefault(n) + 1;

    // min-heap por frequência de tamanho k
    var heap = new PriorityQueue<int, int>();
    foreach (var (num, freq) in count)
    {
        heap.Enqueue(num, freq);
        if (heap.Count > k) heap.Dequeue();
    }
    var result = new int[k];
    for (int i = k - 1; i >= 0; i--) result[i] = heap.Dequeue();
    return result;
}
// TopKFrequent([1,1,1,2,2,3], 2) → [1, 2]
```

---

## Priority Queue com Tuplas

```csharp
// PriorityQueue<TElement, TPriority> — menor prioridade sai primeiro
var pq = new PriorityQueue<string, int>();
pq.Enqueue("baixa prioridade",  1);
pq.Enqueue("alta prioridade",  10);
pq.Enqueue("média prioridade",   5);

string item = pq.Dequeue();
// item = "baixa prioridade" (menor número = maior prioridade no min-heap)

// Para max-priority: negar a prioridade
pq.Enqueue("alta prioridade", -10);
```

---

## Quando Usar Heap

| Problema | Estrutura |
|---|---|
| Running median | Dois heaps (max + min) |
| K maiores/menores elementos | Min-heap de tamanho k |
| Menor entre múltiplas listas ordenadas | Min-heap com índices |
| Dijkstra (menor caminho) | Min-heap com distâncias |
| Agendamento de tarefas | Min-heap com prioridades |
