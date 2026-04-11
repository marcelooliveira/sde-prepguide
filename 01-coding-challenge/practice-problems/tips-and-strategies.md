# Estratégias para o Desafio de Código

Dicas práticas para maximizar sua performance nos 40 minutos do desafio tradicional.

---

## Antes de Começar a Codificar (primeiros 3-5 min)

### 1. Leia o enunciado completamente
- Não comece a codificar na metade da leitura
- Entenda: o que entra? o que deve sair?
- Leia as restrições (tamanho do input, tipos de dados)

### 2. Analise os exemplos
- Trace manualmente pelo menos um exemplo
- Procure padrões e casos especiais
- Se tem um exemplo "edge case", entenda-o

### 3. Identifique o tipo de problema
| Sinal no enunciado | Estrutura provável |
|---|---|
| "menor caminho", "número de passos" | BFS / Dijkstra |
| "todos os caminhos", "existe caminho" | DFS |
| "balanceado", "válido", "aninhado" | Stack |
| "mediana", "k-ésimo maior" | Heap |
| "prefixo", "autocompletar" | Trie |
| "k maiores/menores" | Heap de tamanho k |
| "subarray com soma" | Sliding Window / Prefix Sum |
| "ordenar e comparar" | Sort + Two Pointers |

---

## Durante a Codificação (os ~30 minutos restantes)

### Estruture antes de escrever
1. Escreva os passos principais como comentários
2. Declare as estruturas de dados
3. Implemente passo a passo

```csharp
// Passo 1: construir o grafo
// Passo 2: BFS a partir do nó inicial
// Passo 3: retornar distâncias para todos os outros nós

int[] Solution(int n, int[][] edges, int start)
{
    // --- Passo 1 ---
    var graph = new Dictionary<int, List<int>>();
    for (int i = 1; i <= n; i++) graph[i] = new List<int>();
    // ...
}
```

### Não otimize prematuramente
- Primeiro: faça funcionar (solução correta)
- Depois: verifique a complexidade
- Se O(n²), tente melhorar para O(n log n) ou O(n)

---

## Casos Especiais que Costumam Derrubar Soluções

| Caso | O que verificar |
|---|---|
| Input vazio | `if (arr == null || arr.Length == 0) return ...` |
| Lista com um elemento | Algoritmo funciona para n=1? |
| Todos valores iguais | Não divide por zero, não laço infinito |
| Grafo desconexo | Todos os nós inicializados com -1/inf? |
| Ciclo no grafo | Marcar visitados antes de explorar vizinhos |
| Árvore com apenas raiz | `if (root == null)` e `if (root.Left == null)` |
| Overflow de inteiro | Em C#, use `long` ou `checked {}` para detectar overflow |

---

## Gestão de Tempo (40 minutos)

```
0:00 - 0:05  → Ler e entender o problema
0:05 - 0:10  → Planejar abordagem, identificar estruturas de dados
0:10 - 0:30  → Codificar a solução
0:30 - 0:38  → Testar com os exemplos e corrigir bugs
0:38 - 0:40  → Revisão final de casos especiais
```

> Se estiver preso por mais de 10 minutos, mude abordagem. Não fique travado em otimização sem ter uma solução funcionando primeiro.

---

## Checklist antes de Submeter

- [ ] A saída tem o formato correto? (linha por linha, separado por espaço, etc.)
- [ ] Testei com o exemplo 1?
- [ ] Testei com o exemplo 2?
- [ ] Considerei input vazio ou com um elemento?
- [ ] A complexidade é O(n) ou O(n log n)?
- [ ] O código está legível? (não necessário, mas facilita debugging)

---

## Erros Comuns em C#

```csharp
// ❌ NullReferenceException — acessar propriedade sem verificar null
int val = node.Left.Val; // pode lançar exceção!

// ✅ Verificar null antes de acessar
if (node.Left != null) { int val = node.Left.Val; }

// ❌ Checar visitados DEPOIS de adicionar à fila
queue.Enqueue(neighbor);
if (visited.Contains(neighbor)) { } // tarde demais

// ✅ Checar visitados ANTES de adicionar à fila
if (!visited.Contains(neighbor))
{
    visited.Add(neighbor);
    queue.Enqueue(neighbor);
}

// ❌ Recursão sem caso base
void Dfs(int node)
{
    foreach (int n in graph[node]) Dfs(n); // infinito!
}

// ✅ Com caso base e conjunto de visitados
void Dfs(int node, HashSet<int> visited)
{
    if (visited.Contains(node)) return;
    visited.Add(node);
    foreach (int n in graph[node]) Dfs(n, visited);
}

// ❌ Overflow silencioso em int
int result = 100000 * 100000; // estoura silenciosamente

// ✅ Usar long para valores grandes
long result = (long)100000 * 100000; // correto
```

---

## Como Ler Input no HackerRank (C#)

```csharp
using System;
using System.Linq;

// Ler inteiro
int n = int.Parse(Console.ReadLine());

// Ler lista de inteiros
int[] arr = Console.ReadLine().Split().Select(int.Parse).ToArray();

// Ler múltiplos valores na mesma linha
int[] parts = Console.ReadLine().Split().Select(int.Parse).ToArray();
int a = parts[0], b = parts[1];

// Ler n linhas de pares
var edges = new List<(int, int)>();
for (int i = 0; i < n; i++)
{
    int[] e = Console.ReadLine().Split().Select(int.Parse).ToArray();
    edges.Add((e[0], e[1]));
}
```
