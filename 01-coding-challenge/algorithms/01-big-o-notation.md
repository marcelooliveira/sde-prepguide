# Big O Notation — Análise de Complexidade

Entender Big O é a **base de tudo**. O teste foca em algoritmos O(n) e O(n log n) — soluções mais lentas falham nos casos grandes.

---

## O que é Big O?

Big O descreve o comportamento de um algoritmo em termos de crescimento conforme o tamanho da entrada (`n`) aumenta. Ignoramos constantes e termos menores.

```
f(n) = 3n² + 5n + 2   →   O(n²)
f(n) = 2n log n + 8n  →   O(n log n)
```

---

## Tabela de Complexidades (do mais rápido ao mais lento)

| Notação | Nome | n=10 | n=1000 | n=1.000.000 |
|---|---|---|---|---|
| O(1) | Constante | 1 | 1 | 1 |
| O(log n) | Logarítmica | ~3 | ~10 | ~20 |
| **O(n)** | **Linear** | 10 | 1.000 | 1.000.000 |
| **O(n log n)** | **Linear-log** | ~33 | ~10.000 | ~20.000.000 |
| O(n²) | Quadrática | 100 | 1.000.000 | 10¹² ❌ |
| O(2ⁿ) | Exponencial | 1024 | Inviável ❌ | — |

---

## Exemplos Práticos

### O(1) — Constante
```csharp
int GetFirst(int[] arr)
    => arr[0]; // sempre uma operação

int Lookup(Dictionary<string, int> map, string key)
    => map[key]; // hash lookup
```

### O(log n) — Logarítmica
```csharp
int BinarySearch(int[] arr, int target)
{
    int left = 0, right = arr.Length - 1;
    while (left <= right)
    {
        int mid = left + (right - left) / 2;
        if (arr[mid] == target) return mid;
        else if (arr[mid] < target) left = mid + 1;
        else right = mid - 1;
    }
    return -1;
}
```

### O(n) — Linear
```csharp
int FindMax(int[] arr)
{
    int maxVal = arr[0];
    foreach (int x in arr) // um loop simples = O(n)
        if (x > maxVal) maxVal = x;
    return maxVal;
}
```

### O(n log n) — Linear-log
```csharp
int[] MergeSort(int[] arr)
{
    if (arr.Length <= 1) return arr;
    int mid = arr.Length / 2;
    int[] left  = MergeSort(arr[..mid]);  // log n divisões
    int[] right = MergeSort(arr[mid..]);
    return Merge(left, right);             // n comparações por nível
}
```

### O(n²) — Quadrática (EVITAR)
```csharp
bool HasDuplicate(int[] arr)
{
    for (int i = 0; i < arr.Length; i++)
        for (int j = i + 1; j < arr.Length; j++) // loop dentro de loop
            if (arr[i] == arr[j]) return true;
    return false;
}
// Melhor solução: usar HashSet<int> → O(n)
```

---

## Como Analisar Complexidade

### Regras básicas:

1. **Loops simples** → multiplicam: um loop = O(n), dois loops aninhados = O(n²)
2. **Loops dividindo o problema** → O(log n) (ex: busca binária)
3. **Recursão** → pense na árvore de chamadas
4. **Soma de etapas** → O(n + m) ou simplifique para O(n) se n ≥ m
5. **Descarte constantes** → O(3n) = O(n)

### Exemplos de análise:

```csharp
// O(n + m) — dois loops separados sobre n e m
foreach (int x in listA) Process(x); // O(n)
foreach (int y in listB) Process(y); // O(m)

// O(n * m) — loops aninhados sobre coleções diferentes
foreach (int x in listA)      // O(n)
    foreach (int y in listB)  // O(m) por iteração
        Compare(x, y);
```

---

## Complexidade de Espaço

Além do tempo, avalie o **espaço auxiliar** usado:

| Estrutura | Espaço |
|---|---|
| Array de tamanho n | O(n) |
| Hash map com n entradas | O(n) |
| Recursão com profundidade d | O(d) na call stack |
| Variáveis simples | O(1) |

---

## Dica para o Teste

Antes de submeter, pergunte a si mesmo:
- Minha solução passa para `n = 10⁶`?
- Consigo reduzir de O(n²) para O(n) usando um hash set/map?
- Consigo reduzir de O(n²) para O(n log n) usando ordenação ou heap?
