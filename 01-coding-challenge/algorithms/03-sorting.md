# Ordenação — Algoritmos O(n log n)

O teste foca em complexidade O(n log n). Entender os algoritmos de ordenação ajuda a reconhecer padrões e a escolher a abordagem certa.

---

## Resumo de Complexidades

| Algoritmo | Melhor | Médio | Pior | Espaço | Estável? |
|---|---|---|---|---|---|
| **Merge Sort** | O(n log n) | O(n log n) | O(n log n) | O(n) | Sim |
| **Heap Sort** | O(n log n) | O(n log n) | O(n log n) | O(1) | Não |
| **Quick Sort** | O(n log n) | O(n log n) | O(n²) | O(log n) | Não |
| Introsort (C#) | O(n log n) | O(n log n) | O(n log n) | O(log n) | Não |
| Bubble Sort | O(n) | O(n²) | O(n²) | O(1) | Sim |

> C# usa **Introsort** internamente (`Array.Sort()`, `List.Sort()`). O LINQ `.OrderBy()` usa um stable sort O(n log n). Usar funções built-in é suficiente na maioria dos problemas.

---

## Merge Sort

Divide o array ao meio recursivamente, depois combina as metades ordenadas.

```csharp
int[] MergeSort(int[] arr)
{
    if (arr.Length <= 1) return arr;
    int mid = arr.Length / 2;
    int[] left  = MergeSort(arr[..mid]);
    int[] right = MergeSort(arr[mid..]);
    return Merge(left, right);
}

int[] Merge(int[] left, int[] right)
{
    var result = new List<int>();
    int i = 0, j = 0;
    while (i < left.Length && j < right.Length)
    {
        if (left[i] <= right[j]) result.Add(left[i++]);
        else                     result.Add(right[j++]);
    }
    while (i < left.Length)  result.Add(left[i++]);
    while (j < right.Length) result.Add(right[j++]);
    return result.ToArray();
}
```

**Complexidade:** O(n log n) sempre — log n divisões × n operações de merge por nível  
**Espaço:** O(n) — cria arrays auxiliares

---

## Heap Sort

Usa um heap para ordenar sem espaço extra.

```csharp
int[] HeapSort(int[] arr)
{
    var pq = new PriorityQueue<int, int>();
    foreach (int n in arr) pq.Enqueue(n, n); // O(n log n)
    int[] result = new int[arr.Length];
    for (int i = 0; i < result.Length; i++)
        result[i] = pq.Dequeue(); // O(log n) por elemento
    return result;
}
```

---

## Quick Sort

```csharp
int[] QuickSort(int[] arr)
{
    if (arr.Length <= 1) return arr;
    int pivot = arr[arr.Length / 2];
    var left  = arr.Where(x => x < pivot).ToArray();
    var mid   = arr.Where(x => x == pivot).ToArray();
    var right = arr.Where(x => x > pivot).ToArray();
    return QuickSort(left).Concat(mid).Concat(QuickSort(right)).ToArray();
}
```

> Pior caso O(n²) acontece quando o pivot é sempre o maior/menor. Em produção, usa-se um pivot aleatório com `Random.Shared.Next()`.

---

## Ordenação em C# (Built-in)

```csharp
// Ordenar array in-place
int[] nums = { 3, 1, 4, 1, 5, 9, 2, 6 };
Array.Sort(nums);                                // in-place
int[] sorted = nums.OrderBy(x => x).ToArray();  // nova cópia (LINQ)

// Ordenar com chave customizada
string[] words = { "banana", "apple", "cherry" };
Array.Sort(words, (a, b) => a.Length.CompareTo(b.Length)); // por comprimento
var byLastChar = words.OrderBy(w => w[^1]).ToArray();       // pela última letra

// Ordenar lista de tuplas
var pairs = new List<(int, char)> { (1, 'b'), (2, 'a'), (1, 'a') };
pairs.Sort();                                    // ordem natural (primeiro, segundo)
pairs = pairs.OrderBy(p => p.Item2).ToList();    // só pelo segundo elemento

// Ordenar em ordem reversa
Array.Sort(nums);
Array.Reverse(nums);
// ou via LINQ:
var desc = nums.OrderByDescending(x => x).ToArray();
```

---

## Contagem de Inversões (baseado em Merge Sort)

> Contar quantos pares (i, j) existem onde i < j mas arr[i] > arr[j]

```csharp
(int[] sorted, long inversions) CountInversions(int[] arr)
{
    if (arr.Length <= 1) return (arr, 0);

    int mid = arr.Length / 2;
    var (left,  leftInv)  = CountInversions(arr[..mid]);
    var (right, rightInv) = CountInversions(arr[mid..]);

    var merged = new List<int>();
    long inversions = leftInv + rightInv;
    int i = 0, j = 0;

    while (i < left.Length && j < right.Length)
    {
        if (left[i] <= right[j]) merged.Add(left[i++]);
        else
        {
            merged.Add(right[j++]);
            inversions += left.Length - i; // todos os restantes em left são maiores
        }
    }
    while (i < left.Length)  merged.Add(left[i++]);
    while (j < right.Length) merged.Add(right[j++]);
    return (merged.ToArray(), inversions);
}
```

---

## Bucket Sort / Counting Sort — O(n + k)

Útil quando os valores têm range limitado.

```csharp
int[] CountingSort(int[] arr, int maxVal)
{
    int[] count = new int[maxVal + 1];
    foreach (int num in arr) count[num]++;
    var result = new List<int>();
    for (int val = 0; val <= maxVal; val++)
        for (int f = 0; f < count[val]; f++)
            result.Add(val);
    return result.ToArray();
}
```

---

## Padrões que Requerem Ordenação

| Problema | Técnica |
|---|---|
| Encontrar par com soma alvo | Ordenar + dois ponteiros |
| Intervalos sobrepostos | Ordenar por início |
| Agrupar anagramas | Ordenar cada palavra como chave |
| Kth maior elemento | Quick select ou heap |
| Merge de listas ordenadas | Merge do merge sort + heap |
