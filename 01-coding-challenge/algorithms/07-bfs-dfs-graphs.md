# BFS, DFS e Grafos

Problemas de grafo são **muito frequentes** no SDE II OA. Domine BFS e DFS.

---

## Representação de Grafos

### Lista de Adjacência (preferida para grafos esparsos)
```csharp
// Grafo não-direcionado
var graph = new Dictionary<int, List<int>>
{
    [0] = new List<int> { 1, 2 },
    [1] = new List<int> { 0, 3 },
    [2] = new List<int> { 0, 4 },
    [3] = new List<int> { 1 },
    [4] = new List<int> { 2 }
};

// Construção a partir de lista de arestas
Dictionary<int, List<int>> BuildGraph(int n, int[][] edges)
{
    var g = new Dictionary<int, List<int>>();
    for (int i = 0; i < n; i++) g[i] = new List<int>();
    foreach (var e in edges)
    {
        g[e[0]].Add(e[1]);
        g[e[1]].Add(e[0]); // remova se direcionado
    }
    return g;
}
```

### Matriz de Adjacência (para grafos densos ou quando verificar arestas rápido)
```csharp
// n x n, adj[i,j] = 1 se há aresta entre i e j
int[,] adj = new int[n, n];
adj[0, 1] = adj[1, 0] = 1; // aresta 0-1 (não-direcionado)
```

---

## BFS — Breadth-First Search

**Uso:** menor caminho em grafos não-ponderados, exploração em camadas.

**Complexidade:** O(V + E), onde V = vértices, E = arestas

```csharp
List<int> Bfs(Dictionary<int, List<int>> graph, int start)
{
    var visited = new HashSet<int> { start };
    var queue = new Queue<int>();
    queue.Enqueue(start);
    var order = new List<int>();

    while (queue.Count > 0)
    {
        int node = queue.Dequeue();
        order.Add(node);

        foreach (int neighbor in graph[node])
        {
            if (!visited.Contains(neighbor))
            {
                visited.Add(neighbor);
                queue.Enqueue(neighbor);
            }
        }
    }
    return order;
}
```

### BFS — Menor Caminho (distância entre nós)
```csharp
int BfsShortestPath(Dictionary<int, List<int>> graph, int start, int end)
{
    var visited = new HashSet<int> { start };
    var queue = new Queue<(int node, int dist)>();
    queue.Enqueue((start, 0));

    while (queue.Count > 0)
    {
        var (node, dist) = queue.Dequeue();
        if (node == end) return dist;
        foreach (int neighbor in graph[node])
        {
            if (!visited.Contains(neighbor))
            {
                visited.Add(neighbor);
                queue.Enqueue((neighbor, dist + 1));
            }
        }
    }
    return -1; // sem caminho
}
```

### BFS — Menor Caminho com Reconstrução de Rota
```csharp
List<int> BfsPath(Dictionary<int, List<int>> graph, int start, int end)
{
    var parent = new Dictionary<int, int?> { [start] = null };
    var queue = new Queue<int>();
    queue.Enqueue(start);

    while (queue.Count > 0)
    {
        int node = queue.Dequeue();
        if (node == end) break;
        foreach (int neighbor in graph[node])
        {
            if (!parent.ContainsKey(neighbor))
            {
                parent[neighbor] = node;
                queue.Enqueue(neighbor);
            }
        }
    }

    if (!parent.ContainsKey(end)) return new List<int>();

    var path = new List<int>();
    int? cur = end;
    while (cur != null)
    {
        path.Add(cur.Value);
        cur = parent[cur.Value];
    }
    path.Reverse();
    return path;
}
```

---

## DFS — Depth-First Search

**Uso:** detecção de ciclos, componentes conectados, topological sort, flood fill.

**Complexidade:** O(V + E)

### DFS Iterativo (com pilha)
```csharp
List<int> DfsIterative(Dictionary<int, List<int>> graph, int start)
{
    var visited = new HashSet<int>();
    var stack = new Stack<int>();
    stack.Push(start);
    var order = new List<int>();

    while (stack.Count > 0)
    {
        int node = stack.Pop();
        if (visited.Contains(node)) continue;
        visited.Add(node);
        order.Add(node);
        foreach (int neighbor in graph[node])
            if (!visited.Contains(neighbor))
                stack.Push(neighbor);
    }
    return order;
}
```

### DFS Recursivo
```csharp
void DfsRecursive(Dictionary<int, List<int>> graph, int node, HashSet<int> visited)
{
    visited.Add(node);
    foreach (int neighbor in graph[node])
        if (!visited.Contains(neighbor))
            DfsRecursive(graph, neighbor, visited);
}
```

---

## Componentes Conectados
```csharp
int CountComponents(int n, int[][] edges)
{
    var graph = BuildGraph(n, edges);
    var visited = new HashSet<int>();
    int count = 0;

    for (int node = 0; node < n; node++)
    {
        if (!visited.Contains(node))
        {
            DfsRecursive(graph, node, visited);
            count++;
        }
    }
    return count;
}
```

---

## Algoritmo de Dijkstra — Menor Caminho Ponderado

**Uso:** grafo com pesos **positivos**. Complexidade: O((V + E) log V)

```csharp
// graph[u] = lista de (peso, vizinho)
Dictionary<int, int> Dijkstra(Dictionary<int, List<(int weight, int to)>> graph, int start)
{
    var dist = new Dictionary<int, int>();
    foreach (int node in graph.Keys) dist[node] = int.MaxValue;
    dist[start] = 0;

    // PriorityQueue<elemento, prioridade> — min-heap por padrão
    var pq = new PriorityQueue<(int d, int u), int>();
    pq.Enqueue((0, start), 0);

    while (pq.Count > 0)
    {
        var (d, u) = pq.Dequeue();
        if (d > dist[u]) continue; // caminho obsoleto
        foreach (var (weight, v) in graph[u])
        {
            int newDist = dist[u] + weight;
            if (newDist < dist[v])
            {
                dist[v] = newDist;
                pq.Enqueue((newDist, v), newDist);
            }
        }
    }
    return dist;
}
// Exemplo: graph[0] = [(4,1),(1,2)]; Dijkstra(graph,0) → {0:0, 1:4, 2:1, 3:5}
```

---

## Detecção de Ciclo

### Em grafo não-direcionado (DFS)
```csharp
bool HasCycleUndirected(Dictionary<int, List<int>> graph, int n)
{
    var visited = new HashSet<int>();

    bool Dfs(int node, int parent)
    {
        visited.Add(node);
        foreach (int neighbor in graph[node])
        {
            if (!visited.Contains(neighbor))
            {
                if (Dfs(neighbor, node)) return true;
            }
            else if (neighbor != parent)
                return true; // encontrou ciclo
        }
        return false;
    }

    for (int node = 0; node < n; node++)
        if (!visited.Contains(node))
            if (Dfs(node, -1)) return true;
    return false;
}
```

---

## Checklist de Problemas de Grafo

- [ ] Identificar se é grafo ou árvore
- [ ] Direcionado ou não-direcionado?
- [ ] Ponderado? → Dijkstra. Não-ponderado? → BFS
- [ ] Precisamos de menor caminho? → BFS (não-ponderado) ou Dijkstra
- [ ] Precisamos de todos os nós visitados? → DFS
- [ ] Grafos com ciclos? → DFS com verificação de visitados

---

## Problema Clássico: BFS Shortest Reach

> Dado um grafo não-direcionado, encontre a distância (em múltiplos de 6) de um nó fonte para todos os outros.

```csharp
int[] BfsDistances(int n, int[][] edges, int start)
{
    var graph = new Dictionary<int, List<int>>();
    for (int i = 1; i <= n; i++) graph[i] = new List<int>();
    foreach (var e in edges)
    {
        graph[e[0]].Add(e[1]);
        graph[e[1]].Add(e[0]);
    }

    var dist = new Dictionary<int, int> { [start] = 0 };
    var queue = new Queue<int>();
    queue.Enqueue(start);

    while (queue.Count > 0)
    {
        int node = queue.Dequeue();
        foreach (int neighbor in graph[node])
        {
            if (!dist.ContainsKey(neighbor))
            {
                dist[neighbor] = dist[node] + 6;
                queue.Enqueue(neighbor);
            }
        }
    }

    var result = new List<int>();
    for (int i = 1; i <= n; i++)
        if (i != start)
            result.Add(dist.ContainsKey(i) ? dist[i] : -1);
    return result.ToArray();
}
```
