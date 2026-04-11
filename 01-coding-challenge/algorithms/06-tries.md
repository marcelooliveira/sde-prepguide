# Tries — Árvore de Prefixos

Tries são árvores especializadas para armazenar e buscar strings de forma eficiente. O problema "Contacts" do guia usa diretamente esse conceito.

---

## Conceito

Um **trie** (ou prefix tree) é uma árvore onde cada nó representa um caractere. Caminhos da raiz até um nó marcado formam uma palavra.

```
Palavras: ["apple", "app", "application", "apply"]

root
└── a
    └── p
        └── p  ← fim de "app"
            ├── l
            │   ├── e  ← fim de "apple"
            │   ├── i
            │   │   └── c
            │   │       └── a
            │   │           └── t
            │   │               └── i
            │   │                   └── o
            │   │                       └── n  ← fim de "application"
            │   └── y  ← fim de "apply"
```

---

## Implementação com Dicionário

```csharp
class TrieNode
{
    public Dictionary<char, TrieNode> Children = new();
    public bool IsEnd = false;
    public int Count = 0; // quantas palavras passam por este nó
}

class Trie
{
    private readonly TrieNode root = new();

    public void Insert(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            if (!node.Children.ContainsKey(c))
                node.Children[c] = new TrieNode();
            node = node.Children[c];
            node.Count++;
        }
        node.IsEnd = true;
    }

    // Retorna true se a palavra exata existe
    public bool Search(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            if (!node.Children.ContainsKey(c)) return false;
            node = node.Children[c];
        }
        return node.IsEnd;
    }

    // Retorna true se alguma palavra começa com este prefixo
    public bool StartsWith(string prefix)
    {
        var node = root;
        foreach (char c in prefix)
        {
            if (!node.Children.ContainsKey(c)) return false;
            node = node.Children[c];
        }
        return true;
    }

    // Retorna quantas palavras começam com este prefixo — O(|prefix|)
    public int CountStartsWith(string prefix)
    {
        var node = root;
        foreach (char c in prefix)
        {
            if (!node.Children.ContainsKey(c)) return 0;
            node = node.Children[c];
        }
        return node.Count;
    }
}
```

---

## Problema Clássico: Contacts (do guia)

> Você recebe operações "add name" e "find partial". Para cada "find", retorne quantos contatos começam com o prefixo dado.

```csharp
List<int> SolveContacts(string[][] operations)
{
    var trie = new Trie();
    var results = new List<int>();

    foreach (var op in operations)
    {
        if (op[0] == "add")
            trie.Insert(op[1]);
        else if (op[0] == "find")
            results.Add(trie.CountStartsWith(op[1]));
    }
    return results;
}

// Teste (operações como arrays de strings):
// [["add","hack"],["add","hackerrank"],["find","hac"],["find","hacker"],
//  ["add","harry"],["find","har"],["find","ha"]]
// Resultado: [2, 1, 1, 3]
```

**Complexidade:**
- Insert: O(|word|)
- Search: O(|prefix|)
- Count by prefix: O(|prefix|)

---

## Implementação com Array (mais eficiente para caracteres ASCII)

```csharp
class TrieNodeArray
{
    public TrieNodeArray[] Children = new TrieNodeArray[26]; // letras a-z
    public bool IsEnd = false;
}

class TrieArray
{
    private readonly TrieNodeArray root = new();
    private int Index(char c) => c - 'a';

    public void Insert(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            int i = Index(c);
            node.Children[i] ??= new TrieNodeArray();
            node = node.Children[i];
        }
        node.IsEnd = true;
    }

    public bool Search(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            int i = Index(c);
            if (node.Children[i] == null) return false;
            node = node.Children[i];
        }
        return node.IsEnd;
    }
}
```

---

## Autocompletar com Trie

```csharp
// Adicione este método dentro da classe Trie acima:
public List<string> Autocomplete(string prefix)
{
    var results = new List<string>();
    var node = root;
    foreach (char c in prefix)
    {
        if (!node.Children.ContainsKey(c)) return results;
        node = node.Children[c];
    }

    void Dfs(TrieNode n, string current)
    {
        if (n.IsEnd) results.Add(current);
        foreach (var (c, child) in n.Children)
            Dfs(child, current + c);
    }

    Dfs(node, prefix);
    return results;
}
```

---

## Quando Usar Trie

| Caso de uso | Por que Trie? |
|---|---|
| Busca por prefixo eficiente | O(|prefix|) vs O(n * |word|) em lista |
| Autocompletar / sugestões | Percurso DFS a partir do prefixo |
| Verificar se palavra existe | O(|word|) |
| Contar strings com prefixo comum | Nó com contador |
| Problemas de palavras e subsequências | Trie bidirecional |
