# Árvores Binárias — Conceitos e Implementação

Árvores são estruturas fundamentais no SDE II OA. Os problemas do guia incluem altura, level order traversal e BST.

---

## Estrutura do Nó

```csharp
class TreeNode
{
    public int Val;
    public TreeNode Left, Right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        Val = val; Left = left; Right = right;
    }
}
```

---

## Altura da Árvore (Tree Height)

A altura é o número de arestas no caminho mais longo da raiz até uma folha.

```csharp
int Height(TreeNode root)
{
    if (root == null) return 0;
    int leftHeight  = Height(root.Left);
    int rightHeight = Height(root.Right);
    return 1 + Math.Max(leftHeight, rightHeight);
}
// Exemplo: altura de uma árvore com apenas a raiz = 1
// Altura de null = 0
```

**Complexidade:** O(n) — visita cada nó exatamente uma vez.

---

## Traversais (Percursos)

### Inorder (esquerda → raiz → direita)
> Produz elementos em ordem crescente em uma BST.
```csharp
void Inorder(TreeNode root, List<int> result)
{
    if (root == null) return;
    Inorder(root.Left, result);
    result.Add(root.Val);
    Inorder(root.Right, result);
}
```

### Preorder (raiz → esquerda → direita)
> Útil para copiar ou serializar uma árvore.
```csharp
void Preorder(TreeNode root, List<int> result)
{
    if (root == null) return;
    result.Add(root.Val);
    Preorder(root.Left, result);
    Preorder(root.Right, result);
}
```

### Postorder (esquerda → direita → raiz)
> Útil para deletar uma árvore ou calcular expressões.
```csharp
void Postorder(TreeNode root, List<int> result)
{
    if (root == null) return;
    Postorder(root.Left, result);
    Postorder(root.Right, result);
    result.Add(root.Val);
}
```

---

## Level Order Traversal (BFS em Árvore)

Visita os nós nível por nível, da esquerda para a direita.

```csharp
List<List<int>> LevelOrder(TreeNode root)
{
    var result = new List<List<int>>();
    if (root == null) return result;

    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);

    while (queue.Count > 0)
    {
        int levelSize = queue.Count;
        var level = new List<int>();

        for (int i = 0; i < levelSize; i++)
        {
            var node = queue.Dequeue();
            level.Add(node.Val);
            if (node.Left  != null) queue.Enqueue(node.Left);
            if (node.Right != null) queue.Enqueue(node.Right);
        }
        result.Add(level);
    }
    return result;
}
// Retorna: [[raiz], [nível 1 esq, nível 1 dir], ...]
```

**Complexidade:** O(n) tempo, O(n) espaço (pela fila)

---

## Binary Search Tree (BST)

Propriedade: todo nó à esquerda < raiz < todo nó à direita.

### Inserção
```csharp
TreeNode Insert(TreeNode root, int val)
{
    if (root == null) return new TreeNode(val);
    if (val < root.Val) root.Left  = Insert(root.Left,  val);
    else                root.Right = Insert(root.Right, val);
    return root;
}
```

### Busca
```csharp
TreeNode Search(TreeNode root, int val)
{
    if (root == null || root.Val == val) return root;
    if (val < root.Val) return Search(root.Left,  val);
    return                     Search(root.Right, val);
}
```

### Validar se é BST
```csharp
bool IsValidBst(TreeNode root, long minVal = long.MinValue, long maxVal = long.MaxValue)
{
    if (root == null) return true;
    if (root.Val <= minVal || root.Val >= maxVal) return false;
    return IsValidBst(root.Left,  minVal, root.Val) &&
           IsValidBst(root.Right, root.Val, maxVal);
}
```

---

## Swap Nodes (problema do guia)

> Dado uma árvore, realize trocas entre nós nos níveis que são múltiplos de k, depois execute o traversal inorder.

```csharp
List<List<int>> SwapNodes(int[][] indexes, int[] queries)
{
    int n = indexes.Length;
    int[] left  = new int[n + 2];
    int[] right = new int[n + 2];
    for (int i = 1; i <= n; i++)
    {
        left[i]  = indexes[i - 1][0];
        right[i] = indexes[i - 1][1];
    }

    void SwapAtDepth(int node, int depth, int k)
    {
        if (node == -1) return;
        if (depth % k == 0)
            (left[node], right[node]) = (right[node], left[node]);
        SwapAtDepth(left[node],  depth + 1, k);
        SwapAtDepth(right[node], depth + 1, k);
    }

    void InorderTraversal(int node, List<int> result)
    {
        if (node == -1) return;
        InorderTraversal(left[node],  result);
        result.Add(node);
        InorderTraversal(right[node], result);
    }

    var results = new List<List<int>>();
    foreach (int k in queries)
    {
        SwapAtDepth(1, 1, k);
        var traversal = new List<int>();
        InorderTraversal(1, traversal);
        results.Add(traversal);
    }
    return results;
}
```

---

## Problemas e Propriedades Essenciais

| Problema | Abordagem | Complexidade |
|---|---|---|
| Altura da árvore | DFS recursivo | O(n) |
| Verificar balanceamento | DFS pós-ordem | O(n) |
| Level order traversal | BFS com fila | O(n) |
| Menor ancestral comum (LCA) | DFS recursivo | O(n) |
| Path sum | DFS recursivo | O(n) |
| Inverter árvore | DFS pré-ordem | O(n) |
| Serializar/desserializar | BFS ou preorder | O(n) |

---

## Construir Árvore a partir de Lista

```csharp
TreeNode BuildTree(int?[] values)
{
    // values: array de valores em level order, null para ausente
    if (values == null || values.Length == 0) return null;
    var root = new TreeNode(values[0].Value);
    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);
    int i = 1;
    while (queue.Count > 0 && i < values.Length)
    {
        var node = queue.Dequeue();
        if (i < values.Length && values[i] != null)
        {
            node.Left = new TreeNode(values[i].Value);
            queue.Enqueue(node.Left);
        }
        i++;
        if (i < values.Length && values[i] != null)
        {
            node.Right = new TreeNode(values[i].Value);
            queue.Enqueue(node.Right);
        }
        i++;
    }
    return root;
}
```
