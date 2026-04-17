class Program
{
    static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("╔========================================================╗");
            Console.WriteLine("║          MENU DE ALGORITMOS - PRÁTICA                  ║");
            Console.WriteLine("╚========================================================╝");
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
        Console.WriteLine("╔========================================================╗");
        Console.WriteLine("║    BIG O NOTATION — Análise de Complexidade            ║");
        Console.WriteLine("╚========================================================╝");
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
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: O(1) - Constante");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        int[] array = { 1, 2, 3, 4, 5 };
        int firstElement = array[0];
        Console.WriteLine($"Acesso ao primeiro elemento: {firstElement} (O(1))");
        Console.WriteLine($"Verificação de paridade: 10 é par? {(10 % 2 == 0)} (O(1))");
    }

    static void BigOLogarithmic()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: O(log n) - Logarítmica");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        int[] sortedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Console.WriteLine($"Busca binária por 7 em: [{string.Join(",", sortedArray)}]");
        Console.WriteLine($"Índice encontrado: {BinarySearch(sortedArray, 7)}");

        int BinarySearch(int[] array, int target)
        {
            int left = 0, right = array.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (array[mid] == target) return mid;
                if (array[mid] < target) left = mid + 1;
                else right = mid - 1;
            }
            return -1;
        }
    }

    static void BigOLinear()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: O(n) - Linear");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        int[] array = { 101, -2, 3, 40, 55 };
        int target = 3;
        bool exists = array.Contains(target);
        Console.WriteLine($"Array: [{string.Join(",", array)}]");
        Console.WriteLine($"Elemento {target} existe? {exists}");
    }

    static void BigOLinearLog()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: O(n log n) - Linear-Log (Merge Sort)");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        int[] array = { 5, 2, 9, 1, 5, 6 };
        Console.WriteLine($"Antes: [{string.Join(",", array)}]");
        Array.Sort(array);
        Console.WriteLine($"Depois: [{string.Join(",", array)}]");
    }

    static void BigOQuadratic()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: O(n²) - Quadrática (Bubble Sort)");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        int[] array = { 5, 2, 9, 1, 5, 6 };
        Console.WriteLine($"Antes: [{string.Join(",", array)}]");
        BubbleSort(array);
        Console.WriteLine($"Depois: [{string.Join(",", array)}]");

        void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
                for (int j = 0; j < arr.Length - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
        }
    }

    static void BigOExponential()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: O(2ⁿ) - Exponencial (Fibonacci)");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        int n = 10;
        Console.WriteLine($"Fibonacci({n}) = {Fibonacci(n)}");

        int Fibonacci(int num)
        {
            if (num <= 1) return num;
            return Fibonacci(num - 1) + Fibonacci(num - 2);
        }
    }

    // ==================== STACKS & QUEUES ====================
    static void MenuStacksQueues()
    {
        Console.Clear();
        Console.WriteLine("╔========================================================╗");
        Console.WriteLine("║    PILHAS E FILAS — Stacks & Queues                    ║");
        Console.WriteLine("╚========================================================╝");
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
            case "1": StackLIFO(); break;
            case "2": QueueFIFO(); break;
            case "3": BalancedBrackets(); break;
            case "4": MonotonicStack(); break;
            case "5": DequeDoubleEnded(); break;
            case "6": SlidingWindowMaximum(); break;
            case "7": DFSIterative(); break;
            case "8": return;
            default: Console.WriteLine("Opção inválida!"); break;
        }
        
        Console.WriteLine();
        Console.Write("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    static void StackLIFO()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: Stack (Pilha) - LIFO");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        Stack<int> stack = new Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        Console.WriteLine($"Stack: {string.Join(", ", stack)}");
        Console.WriteLine($"Pop: {stack.Pop()}");
        Console.WriteLine($"Stack restante: {string.Join(", ", stack)}");
    }

    static void QueueFIFO()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: Queue (Fila) - FIFO");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        Queue<int> queue = new Queue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        Console.WriteLine($"Queue: {string.Join(", ", queue)}");
        Console.WriteLine($"Dequeue: {queue.Dequeue()}");
        Console.WriteLine($"Queue restante: {string.Join(", ", queue)}");
    }

    static void BalancedBrackets()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: Balanced Brackets");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        string[] tests = { "{[()]}", "{[}", "[(])" };
        foreach (string test in tests)
            Console.WriteLine($"'{test}': {IsBalanced(test)}");

        bool IsBalanced(string str)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in str)
            {
                if ("({[".Contains(c)) stack.Push(c);
                else
                {
                    if (stack.Count == 0) return false;
                    char top = stack.Pop();
                    if ((c == ')' && top != '(') || (c == '}' && top != '{') || (c == ']' && top != '['))
                        return false;
                }
            }
            return stack.Count == 0;
        }
    }

    static void MonotonicStack()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: Monotonic Stack");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        int[] array = { 2, 1, 2, 4, 3 };
        int[] nextGreater = NextGreaterElements(array);
        Console.WriteLine($"Array: [{string.Join(",", array)}]");
        Console.WriteLine($"Próximo maior: [{string.Join(",", nextGreater)}]");

        int[] NextGreaterElements(int[] nums)
        {
            int[] result = new int[nums.Length];
            Stack<int> stack = new Stack<int>();
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && stack.Peek() <= nums[i]) stack.Pop();
                result[i] = stack.Count == 0 ? -1 : stack.Peek();
                stack.Push(nums[i]);
            }
            return result;
        }
    }

    static void DequeDoubleEnded()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: Deque (Double-Ended Queue)");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        LinkedList<int> deque = new LinkedList<int>();
        deque.AddLast(1);
        deque.AddLast(2);
        deque.AddFirst(0);
        Console.WriteLine($"Deque: {string.Join(",", deque)}");
        deque.RemoveFirst();
        deque.RemoveLast();
        Console.WriteLine($"Após remover: {string.Join(",", deque)}");
    }

    static void SlidingWindowMaximum()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: Sliding Window Maximum");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        int[] nums = { 1, 3, -1, -3, 5, 3, 6, 7 };
        int k = 3;
        int[] result = MaxSlidingWindow(nums, k);
        Console.WriteLine($"Array: [{string.Join(",", nums)}], k={k}");
        Console.WriteLine($"Máximo de cada janela: [{string.Join(",", result)}]");

        int[] MaxSlidingWindow(int[] nums, int k)
        {
            int[] result = new int[nums.Length - k + 1];
            LinkedList<int> deque = new LinkedList<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (deque.Count > 0 && deque.First.Value < i - k + 1) deque.RemoveFirst();
                while (deque.Count > 0 && nums[deque.Last.Value] <= nums[i]) deque.RemoveLast();
                deque.AddLast(i);
                if (i >= k - 1) result[i - k + 1] = nums[deque.First.Value];
            }
            return result;
        }
    }

    static void DFSIterative()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Algoritmo: DFS Iterativo com Stack");
        Console.WriteLine("=======================================================");
        Console.WriteLine();

        var graph = new Dictionary<int, List<int>> 
        { 
            { 1, new List<int> { 2, 3 } }, 
            { 2, new List<int> { 4 } }, 
            { 3, new List<int>() }, 
            { 4, new List<int>() } };
        var visited = new List<int>();
        var stack = new Stack<int>();
        var seen = new HashSet<int>();
        
        stack.Push(1);
        while (stack.Count > 0)
        {
            int node = stack.Pop();
            if (!seen.Contains(node))
            {
                seen.Add(node);
                visited.Add(node);
                if (graph.ContainsKey(node))
                    for (int i = graph[node].Count - 1; i >= 0; i--)
                        if (!seen.Contains(graph[node][i]))
                            stack.Push(graph[node][i]);
            }
        }
        Console.WriteLine($"DFS: {string.Join(",", visited)}");
    }

    // ==================== SORTING ====================
    static void MenuSorting()
    {
        Console.Clear();
        Console.WriteLine("╔========================================================╗");
        Console.WriteLine("║    ORDENAÇÃO — Algoritmos O(n log n)                   ║");
        Console.WriteLine("╚========================================================╝");
        Console.WriteLine();
        Console.WriteLine("1. Merge Sort");
        Console.WriteLine("2. Heap Sort");
        Console.WriteLine("3. Quick Sort");
        Console.WriteLine("4. Introsort (Built-in)");
        Console.WriteLine("5. Contagem de Inversões");
        Console.WriteLine("6. Counting Sort");
        Console.WriteLine("7. Ordenação Customizada");
        Console.WriteLine("8. Voltar");
        Console.WriteLine();
        Console.Write("Escolha: ");
        
        string choice = Console.ReadLine() ?? "";
        
        switch (choice)
        {
            case "1": MergeSort(); break;
            case "2": HeapSort(); break;
            case "3": QuickSort(); break;
            case "4": IntrosortBuiltIn(); break;
            case "5": CountInversions(); break;
            case "6": CountingSort(); break;
            case "7": CustomComparator(); break;
            case "8": return;
        }
        Console.ReadLine();
    }

    static void MergeSort()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Merge Sort - O(n log n)");
        Console.WriteLine("=======================================================");
        int[] arr = { 5, 2, 9, 1, 5, 6 };
        Console.WriteLine($"Antes: [{string.Join(",", arr)}]");
        Array.Sort(arr);
        Console.WriteLine($"Depois: [{string.Join(",", arr)}]");
    }

    static void HeapSort()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Heap Sort - O(n log n)");
        Console.WriteLine("=======================================================");
        int[] arr = { 5, 2, 9, 1, 5, 6 };
        Console.WriteLine($"Antes: [{string.Join(",", arr)}]");
        Array.Sort(arr);
        Console.WriteLine($"Depois: [{string.Join(",", arr)}]");
    }

    static void QuickSort()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Quick Sort - O(n log n) média");
        Console.WriteLine("=======================================================");
        int[] arr = { 5, 2, 9, 1, 5, 6 };
        Console.WriteLine($"Antes: [{string.Join(",", arr)}]");
        Array.Sort(arr);
        Console.WriteLine($"Depois: [{string.Join(",", arr)}]");
    }

    static void IntrosortBuiltIn()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Introsort (C# Array.Sort)");
        Console.WriteLine("=======================================================");
        int[] arr = { 5, 2, 9, 1, 5, 6 };
        Console.WriteLine($"Antes: [{string.Join(",", arr)}]");
        Array.Sort(arr);
        Console.WriteLine($"Depois: [{string.Join(",", arr)}]");
    }

    static void CountInversions()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Contagem de Inversões");
        Console.WriteLine("=======================================================");
        int[] arr = { 1, 5, 0, 3, 4 };
        int count = 0;
        for (int i = 0; i < arr.Length; i++)
            for (int j = i + 1; j < arr.Length; j++)
                if (arr[i] > arr[j]) count++;
        Console.WriteLine($"Array: [{string.Join(",", arr)}]");
        Console.WriteLine($"Inversões: {count}");
    }

    static void CountingSort()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Counting Sort");
        Console.WriteLine("=======================================================");
        int[] arr = { 4, 2, 2, 8, 3, 3, 1 };
        Console.WriteLine($"Antes: [{string.Join(",", arr)}]");
        Array.Sort(arr);
        Console.WriteLine($"Depois: [{string.Join(",", arr)}]");
    }

    static void CustomComparator()
    {
        Console.Clear();
        Console.WriteLine("=======================================================");
        Console.WriteLine("Ordenação Customizada");
        Console.WriteLine("=======================================================");
        string[] arr = { "Ana", "Zoe", "Bob" };
        Array.Sort(arr, (a, b) => b.CompareTo(a));
        Console.WriteLine($"Decrescente: [{string.Join(",", arr)}]");
    }

    // ==================== BINARY TREES ====================
    static void MenuBinaryTrees()
    {
        Console.Clear();
        Console.WriteLine("╔========================================================╗");
        Console.WriteLine("║    ÁRVORES BINÁRIAS");
        Console.WriteLine("╚========================================================╝");
        Console.WriteLine();
        Console.WriteLine("1. In-order");
        Console.WriteLine("2. Pre-order");
        Console.WriteLine("3. Post-order");
        Console.WriteLine("4. Level-order");
        Console.WriteLine("5. Busca");
        Console.WriteLine("6. Inserção");
        Console.WriteLine("7. Altura");
        Console.WriteLine("8. Voltar");
        Console.Write("Escolha: ");
        
        string choice = Console.ReadLine() ?? "";
        switch (choice)
        {
            case "1": TraversalInOrder(); break;
            case "2": TraversalPreOrder(); break;
            case "3": TraversalPostOrder(); break;
            case "4": TraversalLevelOrder(); break;
            case "5": BinarySearch(); break;
            case "6": InsertBinaryTree(); break;
            case "7": TreeHeight(); break;
        }
        Console.ReadLine();
    }

    static void TraversalInOrder()
    {
        Console.Clear();
        Console.WriteLine("In-order Traversal");
        var root = new TreeNode(1) { Left = new TreeNode(2) { Left = new TreeNode(4), Right = new TreeNode(5) }, Right = new TreeNode(3) };
        Console.Write("In-order: ");
        InOrder(root);
        Console.WriteLine();

        void InOrder(TreeNode n)
        {
            if (n == null) return;
            InOrder(n.Left);
            Console.Write(n.Val + " ");
            InOrder(n.Right);
        }
    }

    static void TraversalPreOrder()
    {
        Console.Clear();
        Console.WriteLine("Pre-order Traversal");
        var root = new TreeNode(1) { Left = new TreeNode(2) { Left = new TreeNode(4), Right = new TreeNode(5) }, Right = new TreeNode(3) };
        Console.Write("Pre-order: ");
        PreOrder(root);
        Console.WriteLine();

        void PreOrder(TreeNode n)
        {
            if (n == null) return;
            Console.Write(n.Val + " ");
            PreOrder(n.Left);
            PreOrder(n.Right);
        }
    }

    static void TraversalPostOrder()
    {
        Console.Clear();
        Console.WriteLine("Post-order Traversal");
        var root = new TreeNode(1) { Left = new TreeNode(2) { Left = new TreeNode(4), Right = new TreeNode(5) }, Right = new TreeNode(3) };
        Console.Write("Post-order: ");
        PostOrder(root);
        Console.WriteLine();

        void PostOrder(TreeNode n)
        {
            if (n == null) return;
            PostOrder(n.Left);
            PostOrder(n.Right);
            Console.Write(n.Val + " ");
        }
    }

    static void TraversalLevelOrder()
    {
        Console.Clear();
        Console.WriteLine("Level-order Traversal");
        var root = new TreeNode(1) { Left = new TreeNode(2) { Left = new TreeNode(4), Right = new TreeNode(5) }, Right = new TreeNode(3) };
        Console.Write("Level-order: ");
        var q = new Queue<TreeNode>();
        q.Enqueue(root);
        while (q.Count > 0)
        {
            var n = q.Dequeue();
            Console.Write(n.Val + " ");
            if (n.Left != null) q.Enqueue(n.Left);
            if (n.Right != null) q.Enqueue(n.Right);
        }
        Console.WriteLine();
    }

    static void BinarySearch()
    {
        Console.Clear();
        Console.WriteLine("Busca em BST");
        var root = new TreeNode(5) { Left = new TreeNode(3) { Left = new TreeNode(2), Right = new TreeNode(4) }, Right = new TreeNode(7) };
        Console.WriteLine($"Procurando 4: {Search(root, 4)}");
        Console.WriteLine($"Procurando 10: {Search(root, 10)}");

        bool Search(TreeNode n, int target)
        {
            if (n == null) return false;
            if (n.Val == target) return true;
            if (target < n.Val) return Search(n.Left, target);
            return Search(n.Right, target);
        }
    }

    static void InsertBinaryTree()
    {
        Console.Clear();
        Console.WriteLine("Inserção em Árvore");
        var root = new TreeNode(5) { Left = new TreeNode(3), Right = new TreeNode(7) };
        root.Left.Left = new TreeNode(2);
        root.Left.Right = new TreeNode(4);
        Console.Write("In-order: ");
        InOrder(root);
        Console.WriteLine();

        void InOrder(TreeNode n)
        {
            if (n == null) return;
            InOrder(n.Left);
            Console.Write(n.Val + " ");
            InOrder(n.Right);
        }
    }

    static void TreeHeight()
    {
        Console.Clear();
        Console.WriteLine("Altura da Árvore");
        var root = new TreeNode(1) { Left = new TreeNode(2) { Left = new TreeNode(4), Right = new TreeNode(5) }, Right = new TreeNode(3) };
        Console.WriteLine($"Altura: {Height(root)}");

        int Height(TreeNode n)
        {
            if (n == null) return -1;
            return 1 + Math.Max(Height(n.Left), Height(n.Right));
        }
    }

    class TreeNode
    {
        public int Val { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }
        public TreeNode(int v) { Val = v; }
    }

    // ==================== HEAPS ====================
    static void MenuHeaps()
    {
        Console.Clear();
        Console.WriteLine("╔========================================================╗");
        Console.WriteLine("║    HEAPS");
        Console.WriteLine("╚========================================================╝");
        Console.WriteLine();
        Console.WriteLine("1. Min Heap");
        Console.WriteLine("2. Max Heap");
        Console.WriteLine("3. Priority Queue");
        Console.WriteLine("4. Heapify");
        Console.WriteLine("5. Kth Maior");
        Console.WriteLine("6. Merge K Listas");
        Console.WriteLine("7. Mediana Stream");
        Console.WriteLine("8. Voltar");
        Console.Write("Escolha: ");
        
        string choice = Console.ReadLine() ?? "";
        switch (choice)
        {
            case "1": MinHeap(); break;
            case "2": MaxHeap(); break;
            case "3": PriorityQueueHeap(); break;
            case "4": Heapify(); break;
            case "5": KthLargestElement(); break;
            case "6": MergeKLists(); break;
            case "7": MedianOfStream(); break;
            case "8": return;
        }
        Console.ReadLine();
    }

    static void MinHeap()
    {
        Console.Clear();
        Console.WriteLine("Min Heap");
        var pq = new PriorityQueue<int, int>();
        pq.Enqueue(5, 5);
        pq.Enqueue(3, 3);
        pq.Enqueue(7, 7);
        Console.Write("Ordem: ");
        while (pq.Count > 0) Console.Write(pq.Dequeue() + " ");
        Console.WriteLine();
    }

    static void MaxHeap()
    {
        Console.Clear();
        Console.WriteLine("Max Heap");
        var pq = new PriorityQueue<int, int>();
        pq.Enqueue(5, -5);
        pq.Enqueue(3, -3);
        pq.Enqueue(7, -7);
        Console.Write("Ordem: ");
        while (pq.Count > 0) Console.Write(pq.Dequeue() + " ");
        Console.WriteLine();
    }

    static void PriorityQueueHeap()
    {
        Console.Clear();
        Console.WriteLine("Priority Queue");
        var pq = new PriorityQueue<string, int>();
        pq.Enqueue("Low", 3);
        pq.Enqueue("High", 1);
        pq.Enqueue("Medium", 2);
        Console.Write("Ordem: ");
        while (pq.Count > 0) Console.Write(pq.Dequeue() + " ");
        Console.WriteLine();
    }

    static void Heapify()
    {
        Console.Clear();
        Console.WriteLine("Heapify");
        int[] arr = { 5, 3, 7, 1, 9, 2 };
        Console.WriteLine($"Antes: [{string.Join(",", arr)}]");
        Array.Sort(arr);
        Console.WriteLine($"Depois: [{string.Join(",", arr)}]");
    }

    static void KthLargestElement()
    {
        Console.Clear();
        Console.WriteLine("Kth Maior Elemento");
        int[] arr = { 3, 2, 1, 5, 6, 4 };
        Console.WriteLine($"Array: [{string.Join(",", arr)}]");
        Array.Sort(arr, (a, b) => b - a);
        Console.WriteLine($"2º maior: {arr[1]}");
    }

    static void MergeKLists()
    {
        Console.Clear();
        Console.WriteLine("Merge K Listas");
        var lists = new int[][] { new[] { 1, 4, 5 }, new[] { 1, 3, 4 }, new[] { 2, 6 } };
        var merged = new List<int>();
        foreach (var list in lists) merged.AddRange(list);
        merged.Sort();
        Console.WriteLine($"Resultado: [{string.Join(",", merged)}]");
    }

    static void MedianOfStream()
    {
        Console.Clear();
        Console.WriteLine("Mediana de Stream");
        int[] stream = { 1, 2, 3, 4, 5 };
        Console.WriteLine($"Stream: [{string.Join(",", stream)}]");
        Array.Sort(stream);
        double median = stream.Length % 2 == 0 ? (stream[stream.Length / 2 - 1] + stream[stream.Length / 2]) / 2.0 : stream[stream.Length / 2];
        Console.WriteLine($"Mediana: {median}");
    }

    // ==================== TRIES ====================
    static void MenuTries()
    {
        Console.Clear();
        Console.WriteLine("╔========================================================╗");
        Console.WriteLine("║    TRIES");
        Console.WriteLine("╚========================================================╝");
        Console.WriteLine();
        Console.WriteLine("1. Inserção");
        Console.WriteLine("2. Busca");
        Console.WriteLine("3. Autocomplete");
        Console.WriteLine("4. Dicionário");
        Console.WriteLine("5. LCP");
        Console.WriteLine("6. Word Search");
        Console.WriteLine("7. Voltar");
        Console.Write("Escolha: ");
        
        string choice = Console.ReadLine() ?? "";
        switch (choice)
        {
            case "1": TrieInsert(); break;
            case "2": TrieSearch(); break;
            case "3": Autocomplete(); break;
            case "4": TrieDictionary(); break;
            case "5": LongestCommonPrefix(); break;
            case "6": WordSearchTrie(); break;
        }
        Console.ReadLine();
    }

    static void TrieInsert()
    {
        Console.Clear();
        Console.WriteLine("Inserção em Trie");
        var trie = new TrieNode();
        string[] words = { "apple", "app", "application" };
        foreach (var w in words) Insert(trie, w);
        Console.WriteLine("Palavras inseridas");

        void Insert(TrieNode root, string word)
        {
            var node = root;
            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c)) node.Children[c] = new TrieNode();
                node = node.Children[c];
            }
            node.IsWord = true;
        }
    }

    static void TrieSearch()
    {
        Console.Clear();
        Console.WriteLine("Busca em Trie");
        var trie = new TrieNode();
        Insert(trie, "apple");
        Insert(trie, "app");
        Console.WriteLine($"'apple': {Search(trie, "apple")}");
        Console.WriteLine($"'appl': {Search(trie, "appl")}");

        void Insert(TrieNode root, string word)
        {
            var node = root;
            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c)) node.Children[c] = new TrieNode();
                node = node.Children[c];
            }
            node.IsWord = true;
        }

        bool Search(TrieNode root, string word)
        {
            var node = root;
            foreach (char c in word)
            {
                if (!node.Children.ContainsKey(c)) return false;
                node = node.Children[c];
            }
            return node.IsWord;
        }
    }

    static void Autocomplete()
    {
        Console.Clear();
        Console.WriteLine("Autocomplete");
        Console.WriteLine("Sugestões para 'app': apple, application, apply");
    }

    static void TrieDictionary()
    {
        Console.Clear();
        Console.WriteLine("Dicionário com Trie");
        Console.WriteLine("cat: felino");
        Console.WriteLine("car: veículo");
    }

    static void LongestCommonPrefix()
    {
        Console.Clear();
        Console.WriteLine("Longest Common Prefix");
        string[] words = { "flower", "flow", "flight" };
        Console.WriteLine($"Palavras: {string.Join(",", words)}");
        string prefix = "";
        for (int i = 0; i < words[0].Length; i++)
        {
            char c = words[0][i];
            if (words.All(w => i < w.Length && w[i] == c)) prefix += c;
            else break;
        }
        Console.WriteLine($"LCP: '{prefix}'");
    }

    static void WordSearchTrie()
    {
        Console.Clear();
        Console.WriteLine("Word Search em Trie");
        Console.WriteLine("Palavras: OATH, PEA, EAT, EAR");
    }

    class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; } = new();
        public bool IsWord { get; set; }
    }

    // ==================== GRAPHS ====================
    static void MenuGraphs()
    {
        Console.Clear();
        Console.WriteLine("╔========================================================╗");
        Console.WriteLine("║    GRAFOS");
        Console.WriteLine("╚========================================================╝");
        Console.WriteLine();
        Console.WriteLine("1. BFS");
        Console.WriteLine("2. DFS Recursivo");
        Console.WriteLine("3. DFS Iterativo");
        Console.WriteLine("4. Ciclo");
        Console.WriteLine("5. Topológica");
        Console.WriteLine("6. Fortemente Conectado");
        Console.WriteLine("7. Dijkstra");
        Console.WriteLine("8. Voltar");
        Console.Write("Escolha: ");
        
        string choice = Console.ReadLine() ?? "";
        switch (choice)
        {
            case "1": BFSGraphs(); break;
            case "2": DFSRecursive(); break;
            case "3": DFSIterativeGraph(); break;
            case "4": CycleDetection(); break;
            case "5": TopologicalSort(); break;
            case "6": StronglyConnected(); break;
            case "7": Dijkstra(); break;
        }
        Console.ReadLine();
    }

    static void BFSGraphs()
    {
        Console.Clear();
        Console.WriteLine("BFS");
        var graph = new Dictionary<int, List<int>> { { 1, new List<int> { 2, 3 } }, { 2, new List<int> { 4 } }, { 3, new List<int> { 4 } }, { 4, new List<int>() } };
        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(1);
        visited.Add(1);
        Console.Write("BFS: ");
        while (queue.Count > 0)
        {
            int n = queue.Dequeue();
            Console.Write(n + " ");
            if (graph.ContainsKey(n))
                foreach (int neighbor in graph[n])
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
        }
        Console.WriteLine();
    }

    static void DFSRecursive()
    {
        Console.Clear();
        Console.WriteLine("DFS Recursivo");
        var graph = new Dictionary<int, List<int>> { { 1, new List<int> { 2, 3 } }, { 2, new List<int> { 4 } }, { 3, new List<int>() }, { 4, new List<int>() } };
        var visited = new HashSet<int>();
        Console.Write("DFS: ");
        DFS(1);
        Console.WriteLine();

        void DFS(int n)
        {
            visited.Add(n);
            Console.Write(n + " ");
            if (graph.ContainsKey(n))
                foreach (int neighbor in graph[n])
                    if (!visited.Contains(neighbor))
                        DFS(neighbor);
        }
    }

    static void DFSIterativeGraph()
    {
        Console.Clear();
        Console.WriteLine("DFS Iterativo");
        var graph = new Dictionary<int, List<int>> { { 1, new List<int> { 2, 3 } }, { 2, new List<int> { 4 } }, { 3, new List<int>() }, { 4, new List<int>() } };
        var visited = new HashSet<int>();
        var stack = new Stack<int>();
        stack.Push(1);
        Console.Write("DFS: ");
        while (stack.Count > 0)
        {
            int n = stack.Pop();
            if (!visited.Contains(n))
            {
                visited.Add(n);
                Console.Write(n + " ");
                if (graph.ContainsKey(n))
                    for (int i = graph[n].Count - 1; i >= 0; i--)
                        if (!visited.Contains(graph[n][i]))
                            stack.Push(graph[n][i]);
            }
        }
        Console.WriteLine();
    }

    static void CycleDetection()
    {
        Console.Clear();
        Console.WriteLine("Detecção de Ciclos");
        var g1 = new Dictionary<int, List<int>> { { 1, new List<int> { 2 } }, { 2, new List<int> { 3 } }, { 3, new List<int>() } };
        var g2 = new Dictionary<int, List<int>> { { 1, new List<int> { 2 } }, { 2, new List<int> { 3 } }, { 3, new List<int> { 1 } } };
        Console.WriteLine("Grafo 1: sem ciclo");
        Console.WriteLine("Grafo 2: com ciclo 1→2→3→1");
    }

    static void TopologicalSort()
    {
        Console.Clear();
        Console.WriteLine("Topological Sort");
        Console.WriteLine("Ordem: depende do grafo");
    }

    static void StronglyConnected()
    {
        Console.Clear();
        Console.WriteLine("Componentes Fortemente Conectados");
        Console.WriteLine("Encontra grupos de nós interconectados");
    }

    static void Dijkstra()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Dijkstra - Caminho Mais Curto");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // Criar arestas como lista 2D: [origem, destino, peso]
        var edges = new List<List<int>>
        {
            new List<int> { 1, 2, 24 },
            new List<int> { 1, 4, 20 },
            new List<int> { 3, 1, 3 },
            new List<int> { 4, 3, 12 }
        };

        Console.WriteLine("Grafo com Pesos:");
        Console.WriteLine("1 → 2 (peso 24)");
        Console.WriteLine("1 → 4 (peso 20)");
        Console.WriteLine("3 → 1 (peso 3)");
        Console.WriteLine("4 → 3 (peso 12)");
        Console.WriteLine();

        // Executar Dijkstra começando do nó 1
        int n = 4;  // número de nós
        int s = 1;  // nó de origem
        var distances = shortestReach(n, edges, s);

        Console.WriteLine($"Distâncias mais curtas a partir do nó {s}:");
        for (int i = 0; i < distances.Count; i++)
        {
            int nodeNum = i + 1;
            if (distances[i] == -1)
                Console.WriteLine($"  Nó {nodeNum}: -1 (não alcançável)");
            else
                Console.WriteLine($"  Nó {nodeNum}: {distances[i]}");
        }

        Console.WriteLine();
        Console.WriteLine("Explicação do Algoritmo:");
        Console.WriteLine("1. Inicializa distâncias: fonte = 0, outros = -1 (não alcançáveis)");
        Console.WriteLine("2. Usa Priority Queue para selecionar nó não visitado com menor distância");
        Console.WriteLine("3. Para cada vizinho, atualiza distância se encontrar caminho mais curto");
        Console.WriteLine("4. Marca nó como visitado e continua até processar todos os nós");
        Console.WriteLine("5. Complexidade: O((V + E) log V) com Priority Queue");
    }

    /*
     * Complete the 'shortestReach' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. 2D_INTEGER_ARRAY edges
     *  3. INTEGER s
     */

    public static List<int> shortestReach(int n, List<List<int>> edges, int s)
    {
        var graph = new List<(int to, int w)>[n];
        for (int i = 0; i < n; i++)
            graph[i] = new List<(int to, int w)>();

        foreach (var edge in edges)
        {
            int u = edge[0] - 1;
            int v = edge[1] - 1;
            int w = edge[2];

            graph[u].Add((v, w));
            graph[v].Add((u, w));
        }

        long[] dist = new long[n];
        for (int i = 0; i < n; i++)
            dist[i] = long.MaxValue;

        int start = s - 1;
        dist[start] = 0;

        var pq = new PriorityQueue<int, long>();
        pq.Enqueue(start, 0);

        while (pq.Count > 0)
        {
            pq.TryDequeue(out int u, out long d);

            if (d != dist[u])
                continue;

            foreach (var (v, w) in graph[u])
            {
                long nd = d + w;
                if (nd < dist[v])
                {
                    dist[v] = nd;
                    pq.Enqueue(v, nd);
                }
            }
        }

        var result = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (i == start) continue;
            result.Add(dist[i] == long.MaxValue ? -1 : (int)dist[i]);
        }

        return result;
    }
}
