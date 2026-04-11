# Pilhas e Filas — Stacks & Queues

Estruturas fundamentais para problemas de balanceamento, parsing, BFS e muito mais.

---

## Stack (Pilha) — LIFO

**Last In, First Out** — o último elemento inserido é o primeiro a ser removido.

```csharp
var stack = new Stack<int>();

stack.Push(1); // push
stack.Push(2);
stack.Push(3);

int top = stack.Peek(); // peek: 3
int val = stack.Pop();  // pop: 3

// stack contém: [1, 2] (topo → base)
```

### Operações — Complexidade O(1) amortizado:
| Operação | Método C# | Complexidade |
|---|---|---|
| Push | `stack.Push(x)` | O(1) |
| Pop | `stack.Pop()` | O(1) |
| Peek | `stack.Peek()` | O(1) |
| Is empty | `stack.Count == 0` | O(1) |

---

## Queue (Fila) — FIFO

**First In, First Out** — o primeiro elemento inserido é o primeiro a sair.

```csharp
var queue = new Queue<int>();

queue.Enqueue(1); // enqueue
queue.Enqueue(2);
queue.Enqueue(3);

int front = queue.Peek();    // peek: 1
int val   = queue.Dequeue(); // dequeue: 1

// queue contém: [2, 3]
```

> **`Queue<T>` em C# já é O(1)** para enqueue e dequeue — não há armadilha equivalente ao `List.Remove(0)` do Python.

### Operações com `Queue<T>`:
| Operação | Método C# | Complexidade |
|---|---|---|
| Enqueue | `queue.Enqueue(x)` | O(1) |
| Dequeue | `queue.Dequeue()` | O(1) |
| Peek front | `queue.Peek()` | O(1) |
| Peek back | N/A (`LinkedList<T>` para acesso nos dois extremos) | O(1) |

---

## Problema: Balanced Brackets (do guia)

> Dado uma string com `(`, `)`, `[`, `]`, `{`, `}`, determine se os brackets estão balanceados.

**Abordagem com pilha:**

```csharp
bool IsBalanced(string s)
{
    var stack = new Stack<char>();
    var matching = new Dictionary<char, char>
    {
        [')'] = '(',
        [']'] = '[',
        ['}'] = '{'
    };

    foreach (char c in s)
    {
        if (c == '(' || c == '[' || c == '{')
            stack.Push(c);
        else if (matching.ContainsKey(c))
        {
            if (stack.Count == 0 || stack.Peek() != matching[c])
                return false;
            stack.Pop();
        }
    }
    return stack.Count == 0;
}

// Testes:
// IsBalanced("({[]})") → true
// IsBalanced("({[})")  → false
// IsBalanced("")        → true
// IsBalanced("]")       → false
```

**Complexidade:** O(n) tempo, O(n) espaço

---

## Monotonic Stack (Pilha Monotônica)

Útil para problemas de "próximo maior/menor elemento".

### Next Greater Element
```csharp
int[] NextGreater(int[] arr)
{
    int n = arr.Length;
    int[] result = new int[n];
    Array.Fill(result, -1);
    var stack = new Stack<int>(); // armazena índices

    for (int i = 0; i < n; i++)
    {
        // enquanto o elemento atual for maior que o topo
        while (stack.Count > 0 && arr[i] > arr[stack.Peek()])
        {
            int idx = stack.Pop();
            result[idx] = arr[i];
        }
        stack.Push(i);
    }
    return result;
}
// arr = [4, 1, 2, 3]
// result = [-1, 2, 3, -1]
```

---

## Deque (Double-Ended Queue)

Permite inserção e remoção em ambas as extremidades em O(1).

```csharp
// Em C#, use LinkedList<T> como deque de dupla extremidade
var dq = new LinkedList<int>();
dq.AddLast(1);     // adiciona no fim
dq.AddFirst(0);    // adiciona no início
dq.RemoveLast();   // remove do fim
dq.RemoveFirst();  // remove do início
```

### Sliding Window Maximum (clássico com deque)
```csharp
int[] MaxSlidingWindow(int[] nums, int k)
{
    var result = new List<int>();
    var dq = new LinkedList<int>(); // armazena índices, mantém decrescente

    for (int i = 0; i < nums.Length; i++)
    {
        // remove índices fora da janela
        while (dq.Count > 0 && dq.First.Value < i - k + 1)
            dq.RemoveFirst();

        // remove índices com valores menores (inúteis)
        while (dq.Count > 0 && nums[dq.Last.Value] < nums[i])
            dq.RemoveLast();

        dq.AddLast(i);

        if (i >= k - 1)
            result.Add(nums[dq.First.Value]);
    }
    return result.ToArray();
}
```

---

## Stack para Simulação de Chamadas (Iterativo → Recursivo)

Quando recursão profunda pode causar stack overflow:

```csharp
// DFS iterativo usando Stack<T> explícita
void Dfs(Dictionary<int, List<int>> graph, int start)
{
    var visited = new HashSet<int>();
    var stack = new Stack<int>();
    stack.Push(start);

    while (stack.Count > 0)
    {
        int node = stack.Pop();
        if (visited.Contains(node)) continue;
        visited.Add(node);
        foreach (int neighbor in graph[node])
            stack.Push(neighbor);
    }
}
```

---

## Resumo: Quando Usar Cada Estrutura

| Situação | Estrutura |
|---|---|
| BFS, processamento em ordem de chegada | `Queue<T>` |
| DFS iterativo, undo/redo | `Stack<T>` |
| Brackets balanceados, parsing | `Stack<T>` |
| Próximo maior/menor elemento | `Stack<T>` monotônica |
| Janela deslizante com máximo/mínimo | `LinkedList<T>` (deque) |
