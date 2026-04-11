using System;

class Program
{
    static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║          MENU DE ALGORITMOS - PRÁTICA                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Big O Notation — Análise de Complexidade");
            Console.WriteLine("2. Pilhas e Filas — Stacks & Queues");
            Console.WriteLine("3. Ordenação — Algoritmos O(n log n)");
            Console.WriteLine("4. Árvores Binárias — Binary Trees");
            Console.WriteLine("5. Heaps");
            Console.WriteLine("6. Tries");
            Console.WriteLine("7. BFS / DFS — Grafos");
            Console.WriteLine("8. Sair");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
            
            string choice = Console.ReadLine() ?? "";
            
            switch (choice)
            {
                case "1":
                    MenuBigONotation();
                    break;
                case "2":
                    MenuStacksQueues();
                    break;
                case "3":
                    MenuSorting();
                    break;
                case "4":
                    MenuBinaryTrees();
                    break;
                case "5":
                    MenuHeaps();
                    break;
                case "6":
                    MenuTries();
                    break;
                case "7":
                    MenuGraphs();
                    break;
                case "8":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadLine();
                    break;
            }
        }
    }

    // ==================== BIG O NOTATION ====================
    static void MenuBigONotation()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    BIG O NOTATION — Análise de Complexidade            ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("1. O(1) - Constante");
        Console.WriteLine("2. O(log n) - Logarítmica");
        Console.WriteLine("3. O(n) - Linear");
        Console.WriteLine("4. O(n log n) - Linear-Log");
        Console.WriteLine("5. O(n²) - Quadrática");
        Console.WriteLine("6. O(2ⁿ) - Exponencial");
        Console.WriteLine("7. Voltar ao Menu Principal");
        Console.WriteLine();
        Console.Write("Escolha uma opção: ");
        
        string choice = Console.ReadLine() ?? "";
        
        switch (choice)
        {
            case "1":
                BigOConstant();
                break;
            case "2":
                BigOLogarithmic();
                break;
            case "3":
                BigOLinear();
                break;
            case "4":
                BigOLinearLog();
                break;
            case "5":
                BigOQuadratic();
                break;
            case "6":
                BigOExponential();
                break;
            case "7":
                return;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
        
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    static void BigOConstant()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(1) - Constante");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para O(1) - Constante
    }

    static void BigOLogarithmic()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(log n) - Logarítmica");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para O(log n) - Logarítmica
    }

    static void BigOLinear()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(n) - Linear");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para O(n) - Linear
    }

    static void BigOLinearLog()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(n log n) - Linear-Log");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para O(n log n) - Linear-Log
    }

    static void BigOQuadratic()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(n²) - Quadrática");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para O(n²) - Quadrática
    }

    static void BigOExponential()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(2ⁿ) - Exponencial");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para O(2ⁿ) - Exponencial
    }

    // ==================== STACKS & QUEUES ====================
    static void MenuStacksQueues()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    PILHAS E FILAS — Stacks & Queues                    ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("1. Stack (Pilha) - LIFO");
        Console.WriteLine("2. Queue (Fila) - FIFO");
        Console.WriteLine("3. Balanced Brackets");
        Console.WriteLine("4. Monotonic Stack");
        Console.WriteLine("5. Deque (Double-Ended Queue)");
        Console.WriteLine("6. Sliding Window Maximum");
        Console.WriteLine("7. DFS Iterativo com Stack");
        Console.WriteLine("8. Voltar ao Menu Principal");
        Console.WriteLine();
        Console.Write("Escolha uma opção: ");
        
        string choice = Console.ReadLine() ?? "";
        
        switch (choice)
        {
            case "1":
                StackLIFO();
                break;
            case "2":
                QueueFIFO();
                break;
            case "3":
                BalancedBrackets();
                break;
            case "4":
                MonotonicStack();
                break;
            case "5":
                DequeDoubleEnded();
                break;
            case "6":
                SlidingWindowMaximum();
                break;
            case "7":
                DFSIterative();
                break;
            case "8":
                return;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
        
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    static void StackLIFO()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Stack (Pilha) - LIFO");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Stack (LIFO)
    }

    static void QueueFIFO()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Queue (Fila) - FIFO");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Queue (FIFO)
    }

    static void BalancedBrackets()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Balanced Brackets");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Balanced Brackets
    }

    static void MonotonicStack()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Monotonic Stack");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Monotonic Stack
    }

    static void DequeDoubleEnded()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Deque (Double-Ended Queue)");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Deque
    }

    static void SlidingWindowMaximum()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Sliding Window Maximum");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Sliding Window Maximum
    }

    static void DFSIterative()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: DFS Iterativo com Stack");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para DFS Iterativo
    }

    // ==================== SORTING ====================
    static void MenuSorting()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    ORDENAÇÃO — Algoritmos O(n log n)                   ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("1. Merge Sort");
        Console.WriteLine("2. Heap Sort");
        Console.WriteLine("3. Quick Sort");
        Console.WriteLine("4. Introsort (C# Built-in)");
        Console.WriteLine("5. Contagem de Inversões");
        Console.WriteLine("6. Counting Sort / Bucket Sort");
        Console.WriteLine("7. Ordenação com Comparador Customizado");
        Console.WriteLine("8. Voltar ao Menu Principal");
        Console.WriteLine();
        Console.Write("Escolha uma opção: ");
        
        string choice = Console.ReadLine() ?? "";
        
        switch (choice)
        {
            case "1":
                MergeSort();
                break;
            case "2":
                HeapSort();
                break;
            case "3":
                QuickSort();
                break;
            case "4":
                IntrosortBuiltIn();
                break;
            case "5":
                CountInversions();
                break;
            case "6":
                CountingSort();
                break;
            case "7":
                CustomComparator();
                break;
            case "8":
                return;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
        
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    static void MergeSort()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Merge Sort");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Merge Sort
    }

    static void HeapSort()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Heap Sort");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Heap Sort
    }

    static void QuickSort()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Quick Sort");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Quick Sort
    }

    static void IntrosortBuiltIn()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Introsort (C# Built-in)");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Introsort
    }

    static void CountInversions()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Contagem de Inversões");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Contagem de Inversões
    }

    static void CountingSort()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Counting Sort / Bucket Sort");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Counting Sort
    }

    static void CustomComparator()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Ordenação com Comparador Customizado");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Ordenação Customizada
    }

    // ==================== BINARY TREES ====================
    static void MenuBinaryTrees()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    ÁRVORES BINÁRIAS — Binary Trees                     ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("1. Traversal - In-order");
        Console.WriteLine("2. Traversal - Pre-order");
        Console.WriteLine("3. Traversal - Post-order");
        Console.WriteLine("4. Level-order (BFS)");
        Console.WriteLine("5. Busca Binária em Árvore");
        Console.WriteLine("6. Inserção em Árvore Binária");
        Console.WriteLine("7. Altura da Árvore");
        Console.WriteLine("8. Voltar ao Menu Principal");
        Console.WriteLine();
        Console.Write("Escolha uma opção: ");
        
        string choice = Console.ReadLine() ?? "";
        
        switch (choice)
        {
            case "1":
                TraversalInOrder();
                break;
            case "2":
                TraversalPreOrder();
                break;
            case "3":
                TraversalPostOrder();
                break;
            case "4":
                TraversalLevelOrder();
                break;
            case "5":
                BinarySearch();
                break;
            case "6":
                InsertBinaryTree();
                break;
            case "7":
                TreeHeight();
                break;
            case "8":
                return;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
        
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    static void TraversalInOrder()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Traversal - In-order");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Traversal In-order
    }

    static void TraversalPreOrder()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Traversal - Pre-order");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Traversal Pre-order
    }

    static void TraversalPostOrder()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Traversal - Post-order");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Traversal Post-order
    }

    static void TraversalLevelOrder()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Level-order (BFS)");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Traversal Level-order
    }

    static void BinarySearch()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Busca Binária em Árvore");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Busca Binária
    }

    static void InsertBinaryTree()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Inserção em Árvore Binária");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Inserção
    }

    static void TreeHeight()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Altura da Árvore");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Altura da Árvore
    }

    // ==================== HEAPS ====================
    static void MenuHeaps()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    HEAPS                                               ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("1. Min Heap");
        Console.WriteLine("2. Max Heap");
        Console.WriteLine("3. Priority Queue");
        Console.WriteLine("4. Heapify");
        Console.WriteLine("5. Kth Maior Elemento");
        Console.WriteLine("6. Merge de K Listas Ordenadas");
        Console.WriteLine("7. Mediana de Stream");
        Console.WriteLine("8. Voltar ao Menu Principal");
        Console.WriteLine();
        Console.Write("Escolha uma opção: ");
        
        string choice = Console.ReadLine() ?? "";
        
        switch (choice)
        {
            case "1":
                MinHeap();
                break;
            case "2":
                MaxHeap();
                break;
            case "3":
                PriorityQueueHeap();
                break;
            case "4":
                Heapify();
                break;
            case "5":
                KthLargestElement();
                break;
            case "6":
                MergeKLists();
                break;
            case "7":
                MedianOfStream();
                break;
            case "8":
                return;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
        
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    static void MinHeap()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Min Heap");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Min Heap
    }

    static void MaxHeap()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Max Heap");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Max Heap
    }

    static void PriorityQueueHeap()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Priority Queue");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Priority Queue
    }

    static void Heapify()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Heapify");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Heapify
    }

    static void KthLargestElement()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Kth Maior Elemento");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Kth Maior Elemento
    }

    static void MergeKLists()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Merge de K Listas Ordenadas");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Merge K Listas
    }

    static void MedianOfStream()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Mediana de Stream");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Mediana de Stream
    }

    // ==================== TRIES ====================
    static void MenuTries()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    TRIES                                               ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("1. Inserção em Trie");
        Console.WriteLine("2. Busca em Trie");
        Console.WriteLine("3. Autocomplete");
        Console.WriteLine("4. Dicionário com Trie");
        Console.WriteLine("5. Longest Common Prefix");
        Console.WriteLine("6. Word Search em Trie");
        Console.WriteLine("7. Voltar ao Menu Principal");
        Console.WriteLine();
        Console.Write("Escolha uma opção: ");
        
        string choice = Console.ReadLine() ?? "";
        
        switch (choice)
        {
            case "1":
                TrieInsert();
                break;
            case "2":
                TrieSearch();
                break;
            case "3":
                Autocomplete();
                break;
            case "4":
                TrieDictionary();
                break;
            case "5":
                LongestCommonPrefix();
                break;
            case "6":
                WordSearchTrie();
                break;
            case "7":
                return;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
        
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    static void TrieInsert()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Inserção em Trie");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Inserção em Trie
    }

    static void TrieSearch()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Busca em Trie");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Busca em Trie
    }

    static void Autocomplete()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Autocomplete");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Autocomplete
    }

    static void TrieDictionary()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Dicionário com Trie");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Dicionário com Trie
    }

    static void LongestCommonPrefix()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Longest Common Prefix");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Longest Common Prefix
    }

    static void WordSearchTrie()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Word Search em Trie");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Word Search
    }

    // ==================== GRAPHS - BFS/DFS ====================
    static void MenuGraphs()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    BFS / DFS — GRAFOS                                  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.WriteLine();
        Console.WriteLine("1. BFS (Breadth-First Search)");
        Console.WriteLine("2. DFS (Depth-First Search) - Recursivo");
        Console.WriteLine("3. DFS (Depth-First Search) - Iterativo");
        Console.WriteLine("4. Detecção de Ciclos");
        Console.WriteLine("5. Topological Sort");
        Console.WriteLine("6. Componentes Fortemente Conectados");
        Console.WriteLine("7. Dijkstra - Caminho Mais Curto");
        Console.WriteLine("8. Voltar ao Menu Principal");
        Console.WriteLine();
        Console.Write("Escolha uma opção: ");
        
        string choice = Console.ReadLine() ?? "";
        
        switch (choice)
        {
            case "1":
                BFSGraphs();
                break;
            case "2":
                DFSRecursive();
                break;
            case "3":
                DFSIterativeGraph();
                break;
            case "4":
                CycleDetection();
                break;
            case "5":
                TopologicalSort();
                break;
            case "6":
                StronglyConnected();
                break;
            case "7":
                Dijkstra();
                break;
            case "8":
                return;
            default:
                Console.WriteLine("Opção inválida!");
                break;
        }
        
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    static void BFSGraphs()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: BFS (Breadth-First Search)");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para BFS
    }

    static void DFSRecursive()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: DFS (Depth-First Search) - Recursivo");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para DFS Recursivo
    }

    static void DFSIterativeGraph()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: DFS (Depth-First Search) - Iterativo");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para DFS Iterativo
    }

    static void CycleDetection()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Detecção de Ciclos");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Detecção de Ciclos
    }

    static void TopologicalSort()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Topological Sort");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Topological Sort
    }

    static void StronglyConnected()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Componentes Fortemente Conectados");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Componentes Fortemente Conectados
    }

    static void Dijkstra()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Dijkstra - Caminho Mais Curto");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // TODO: Implemente aqui o código para Dijkstra
    }
}
