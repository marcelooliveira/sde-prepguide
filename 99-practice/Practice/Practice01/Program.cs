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

        //Definição:
        // O(1) representa um algoritmo cuja complexidade de tempo é constante, ou seja,
        // o tempo de execução não depende do tamanho da entrada.
        // Ele sempre executa em um tempo fixo, independentemente do número de elementos processados.

        // TODO: Implemente aqui o código para O(1) - Constante

        //Exemplo 1:
        int[] array = { 1, 2, 3, 4, 5 };
        int firstElement = array[0]; // Acesso ao primeiro elemento é O(1)
        Console.WriteLine(
            $"Acesso ao primeiro elemento do array: {firstElement} (O(1))"
        );

        //Exemplo 2:
        Console.WriteLine(
            $"Impressão de uma mensagem fixa: 'Hello, World!' (O(1))"
        );

        //Exemplo 3:
        Console.WriteLine(
            $"Verificação de paridade de um número: 10 é par? {(10 % 2 == 0)} (O(1))"
        );
    }

    static void BigOLogarithmic()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(log n) - Logarítmica");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        //Definição
        // O(log n) representa um algoritmo cuja complexidade de tempo é logarítmica, ou seja,
        // o tempo de execução cresce de forma logarítmica em relação ao tamanho da entrada.
        // Ele é mais eficiente do que O(n) para grandes entradas, pois reduz o número de operações necessárias.

        // TODO: Implemente aqui o código para O(log n) - Logarítmica

        // Exemplo 1:Busca Binária
        int[] sortedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int target = 7;
        int left = 0;
        int right = sortedArray.Length - 1;
        Console.WriteLine(
            $"Busca binária por {target} em um array ordenado (O(log n))"
        );
        Console.WriteLine(
            $"Índice encontrado: {BinarySearch(sortedArray, target)}"
        );

        int BinarySearch( int[] array, int target )
        {
            int left = 0;
            int right = array.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (array[mid] == target)
                    return mid;
                else if (array[mid] < target)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return -1; // Elemento não encontrado
        }
    }

    static void BigOLinear()
    {
        Console.Clear();
        Console.WriteLine("Algoritmo: O(n) - Linear");
        Console.WriteLine();

        //Definição:
        // O(n) representa um algoritmo cuja complexidade de tempo é linear, ou seja,
        // o tempo de execução cresce linearmente em relação ao tamanho da entrada.

        // TODO: Implemente aqui o código para O(n) - Linear

        //Exemplo 1: Soma de elementos em um array
        Console.WriteLine(
            $"Soma de elementos em um array (O(n))"
        );

        //Exemplo 2: Verificação de existência de um elemento em um array
        int[] array = { 101, -2, 3, 40, 55 };
        int target = 3;
        Console.WriteLine(
            $"Verificação de existência de {target} em um array (O(n))"
        );
        
        bool exists = false;
        foreach (int num in array)
        {
            if (num == target)
            {
                exists = true;
                break;
            }
        }
        Console.WriteLine(
            $"Elemento {target} existe no array? {exists}"
        );
    }

    static void BigOLinearLog()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(n log n) - Linear-Log");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        //Descrição:
        // O(n log n) representa um algoritmo cuja complexidade de tempo é linear-logarítmica, ou seja, o tempo de execução cresce em relação
        // ao produto do tamanho da entrada e o logaritmo do tamanho da entrada.

        // TODO: Implemente aqui o código para O(n log n) - Linear-Log

        //Exemplo 1: Merge Sort
        Console.WriteLine(
            $"Ordenação de um array usando Merge Sort (O(n log n))"
        );
        Console.WriteLine(
            $"Array antes da ordenação: [5, 2, 9, 1, 5, 6]"
        );
          int[] array = { 5, 2, 9, 1, 5, 6 };
        MergeSort(array);
        Console.WriteLine(
            $"Array após a ordenação: [{string.Join(", ", array)}]"
        );
        Console.WriteLine(
            $"Merge Sort é um algoritmo de ordenação eficiente com complexidade O(n log n)"
        );
        Console.WriteLine(
            $"Ele divide o array em subarrays menores, ordena cada subarray e depois os combina para formar o array ordenado final."
        );

        void MergeSort( int[] arr )
          {
              if (arr.Length <= 1)
                  return;
  
              int mid = arr.Length / 2;
              int[] left = new int[mid];
              int[] right = new int[arr.Length - mid];
  
              Array.Copy(arr, 0, left, 0, mid);
              Array.Copy(arr, mid, right, 0, arr.Length - mid);
  
              MergeSort(left);
              MergeSort(right);
              Merge(left, right, arr);
        }

        void Merge( int[] left, int[] right, int[] result )
        {
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                    result[k++] = left[i++];
                else
                    result[k++] = right[j++];
            }
            while (i < left.Length)
                result[k++] = left[i++];
            while (j < right.Length)
                result[k++] = right[j++];
        }
    }

    static void BigOQuadratic()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(n²) - Quadrática");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // Descrição:
        // O(n²) representa um algoritmo cuja complexidade de tempo é quadrática,
        // ou seja, o tempo de execução cresce proporcionalmente ao quadrado do tamanho da entrada.

        // TODO: Implemente aqui o código para O(n²) - Quadrática

        // Exemplo 1: Bubble Sort
        Console.WriteLine(
            $"Ordenação de um array usando Bubble Sort (O(n²))"
        );
        Console.WriteLine(
            $"Array antes da ordenação: [5, 2, 9, 1, 5, 6]"
        );
        int[] array = { 5, 2, 9, 1, 5, 6 };
        BubbleSort(array);
        Console.WriteLine(
            $"Array após a ordenação: [{string.Join(", ", array)}]"
        );
        Console.WriteLine(
            $"Bubble Sort é um algoritmo de ordenação simples, mas ineficiente para grandes conjuntos de dados, com complexidade O(n²)"
        );
        void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        // Troca arr[j] e arr[j + 1]
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }
    }

    static void BigOExponential()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: O(2ⁿ) - Exponencial");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        //Descrição:
        // O(2ⁿ) representa um algoritmo cuja complexidade de tempo é exponencial, ou seja,
        // o tempo de execução cresce exponencialmente em relação ao tamanho da entrada.

        // TODO: Implemente aqui o código para O(2ⁿ) - Exponencial
        //Exemplo 1: Fibonacci Recursivo
        Console.WriteLine(
            $"Cálculo do n-ésimo número de Fibonacci usando recursão (O(2ⁿ))"
        );
        int n = 30; // Cuidado: valores maiores podem levar a tempos de execução muito longos
        Console.WriteLine(
            $"O {n}-ésimo número de Fibonacci é: {Fibonacci(n)}"
        );
        Console.WriteLine(
            $"O algoritmo recursivo para Fibonacci tem complexidade O(2ⁿ) devido à grande quantidade de chamadas recursivas redundantes."
        );
        int Fibonacci(int num)
        {
            if (num <= 1)
                return num;
            return Fibonacci(num - 1) + Fibonacci(num - 2);
        }
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

        // Definição:
        // Stack (Pilha) é uma estrutura de dados que segue a ordem LIFO (Last In, First Out),
        // ou seja, o último elemento inserido é o primeiro a ser removido.

        // TODO: Implemente aqui o código para Stack (LIFO)

        // Exemplo 1: Implementação de uma pilha usando array
        Console.WriteLine(
            $"Implementação de uma pilha usando array (LIFO)"
        );
        Stack<int> stack = new Stack<int>();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        Console.WriteLine(
            $"Elementos na pilha: {string.Join(", ", stack)}"
        );

        // Exemplo 2: Uso de pilha para inverter uma string
        string input = "Hello, World!";
        string reversed = ReverseString(input);
        Console.WriteLine(
            $"String original: {input}"
        );
        Console.WriteLine(
            $"String invertida usando pilha: {reversed}"
        );
        string ReverseString(string str)
        {
            Stack<char> charStack = new Stack<char>();
            foreach (char c in str)
                charStack.Push(c);
            char[] reversedChars = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
                reversedChars[i] = charStack.Pop();
            return new string(reversedChars);
        }
    }

    static void QueueFIFO()
    {
        Console.Clear();
        Console.WriteLine("Algoritmo: Queue (Fila) - FIFO");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // Descrição:
        // Queue (Fila) é uma estrutura de dados que segue a ordem FIFO (First In, First Out),
        // ou seja, o primeiro elemento inserido é o primeiro a ser removido.

        // TODO: Implemente aqui o código para Queue (FIFO)

        // Exemplo 1: Implementação de uma fila usando array
        Console.WriteLine(
            $"Implementação de uma fila usando array (FIFO)"
        );
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        Console.WriteLine(
            $"Elementos na fila: {string.Join(", ", queue)}"
        );
        // Exemplo 2: Uso de fila para simular uma fila de atendimento
        Console.WriteLine(
            $"Simulação de uma fila de atendimento usando Queue"
        );
        Queue<string> atendimentoQueue = new Queue<string>();
        atendimentoQueue.Enqueue("Cliente A");
        atendimentoQueue.Enqueue("Cliente B");
        atendimentoQueue.Enqueue("Cliente C");
        Console.WriteLine(
            $"Clientes na fila de atendimento: {string.Join(", ", atendimentoQueue)}"
        );
        Console.WriteLine(
            $"Atendendo o próximo cliente: {atendimentoQueue.Dequeue()}"
        );
        Console.WriteLine(
            $"Clientes restantes na fila de atendimento: {string.Join(", ", atendimentoQueue)}"
        );
        Console.WriteLine(
            $"Queue é uma estrutura de dados útil para gerenciar tarefas em ordem de chegada, como filas de impressão, atendimento ao cliente, etc."
        );
    }

    static void BalancedBrackets()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Balanced Brackets");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // Descrição:
        // O algoritmo de Balanced Brackets verifica se os parênteses, colchetes e chaves em uma string estão balanceados,
        // ou seja, cada abertura tem um fechamento correspondente na ordem correta.

        // TODO: Implemente aqui o código para Balanced Brackets

        // Exemplo 1: Verificação de uma string com parênteses balanceados
        string input = "{[()]}";
        Console.WriteLine(
            $"Verificação de parênteses balanceados para a string: {input}"
        );
        Console.WriteLine(
            $"A string é balanceada? {IsBalanced(input)}"
        );
        Console.WriteLine(
            $"O algoritmo utiliza uma pilha para armazenar os caracteres de abertura e verifica se cada caractere de fechamento corresponde ao topo da pilha."
        );
        Console.WriteLine(
            $"Se a pilha estiver vazia no final da verificação, a string é considerada balanceada."
        );
        Console.WriteLine(
            $"Exemplo de string não balanceada: {input + "]"}"
        );
        Console.WriteLine(
            $"A string é balanceada? {IsBalanced(input + "]")}"
        );
        Console.WriteLine(
            $"Exemplo de string não balanceada: {input + "{"}"
        );

        bool IsBalanced(string str)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char c in str)
            {
                if (c == '(' || c == '{' || c == '[')
                    stack.Push(c);
                else if (c == ')' || c == '}' || c == ']')
                {
                    if (stack.Count == 0)
                        return false;
                    char top = stack.Pop();
                    if ((c == ')' && top != '(') ||
                        (c == '}' && top != '{') ||
                        (c == ']' && top != '['))
                        return false;
                }
            }
            return stack.Count == 0;
        }
    }

    static void MonotonicStack()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Monotonic Stack");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // Descrição:
        // Monotonic Stack é uma estrutura de dados que mantém os elementos em ordem crescente ou decrescente,
        // permitindo resolver problemas como o próximo maior elemento, próximo menor elemento, etc.

        // TODO: Implemente aqui o código para Monotonic Stack

        // Exemplo 1: Encontrar o próximo maior elemento para cada elemento em um array
        Console.WriteLine(
            $"Encontrar o próximo maior elemento para cada elemento em um array usando Monotonic Stack"
        );
        int[] array = { 2, 1, 2, 4, 3 };
        int[] nextGreater = NextGreaterElements(array);
        Console.WriteLine(
            $"Array original: [{string.Join(", ", array)}]"
        );
        Console.WriteLine(
            $"Próximo maior elemento para cada posição: [{string.Join(", ", nextGreater)}]"
        );
        int[] NextGreaterElements(int[] nums)
        {
            int n = nums.Length;
            int[] result = new int[n];
            Stack<int> stack = new Stack<int>();
            for (int i = n - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && stack.Peek() <= nums[i])
                    stack.Pop();
                result[i] = stack.Count == 0 ? -1 : stack.Peek();
                stack.Push(nums[i]);
            }
            return result;
        }
    }

    static void DequeDoubleEnded()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Deque (Double-Ended Queue)");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // Descrição:
        // Deque (Double-Ended Queue) é uma estrutura de dados que permite inserção
        // e remoção de elementos tanto no início quanto no final da fila.

        // TODO: Implemente aqui o código para Deque

        // Exemplo 1: Implementação de um deque usando LinkedList
        Console.WriteLine(
            $"Implementação de um deque usando LinkedList"
        );
        LinkedList<int> deque = new LinkedList<int>();
        // Inserção no final
        deque.AddLast(1);
        deque.AddLast(2);
        // Inserção no início
        deque.AddFirst(0);
        deque.AddFirst(1);
        Console.WriteLine(
            $"Elementos no deque: {string.Join(", ", deque)}"
        );
        // Remoção do início
        deque.RemoveFirst();
        // Remoção do final
        deque.RemoveLast();
        Console.WriteLine(
            $"Elementos no deque após remoções: {string.Join(", ", deque)}"
        );

        Console.WriteLine(
            $"Deque é uma estrutura de dados versátil que pode ser usada para implementar filas, pilhas e outras estruturas de dados."
        );
    }

    static void SlidingWindowMaximum()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: Sliding Window Maximum");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // Descrição: O algoritmo Sliding Window Maximum encontra o valor máximo em uma janela deslizante de tamanho fixo em um array.
        // Usa um Deque para manter os índices dos elementos em ordem decrescente de valor.

        Console.WriteLine("Encontrar o máximo em cada janela deslizante de tamanho 3");
        int[] nums = { 1, 3, -1, -3, 5, 3, 6, 7 };
        int k = 3;
        int[] result = SlidingWindowMaximumHelper(nums, k);
        
        Console.WriteLine($"Array: [{string.Join(", ", nums)}]");
        Console.WriteLine($"Tamanho da janela: {k}");
        Console.WriteLine($"Máximo de cada janela: [{string.Join(", ", result)}]");
        Console.WriteLine();
        Console.WriteLine("Explicação do algoritmo:");
        Console.WriteLine("- Usa um Deque para armazenar índices dos elementos");
        Console.WriteLine("- Mantém os índices em ordem decrescente de valor");
        Console.WriteLine("- O primeiro elemento do Deque é sempre o índice do máximo");
        Console.WriteLine("- Complexidade: O(n) - cada elemento é adicionado e removido uma vez");

        int[] SlidingWindowMaximumHelper(int[] nums, int k)
        {
            if (nums.Length == 0) return new int[0];
            
            int[] result = new int[nums.Length - k + 1];
            LinkedList<int> deque = new LinkedList<int>(); // Armazena índices
            
            for (int i = 0; i < nums.Length; i++)
            {
                // Remove índices fora da janela atual
                if (deque.Count > 0 && deque.First.Value < i - k + 1)
                    deque.RemoveFirst();
                
                // Remove elementos menores do final do deque
                while (deque.Count > 0 && nums[deque.Last.Value] <= nums[i])
                    deque.RemoveLast();
                
                // Adiciona o índice atual
                deque.AddLast(i);
                
                // Armazena o máximo da janela
                if (i >= k - 1)
                    result[i - k + 1] = nums[deque.First.Value];
            }
            
            return result;
        }
    }

    static void DFSIterative()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine("Algoritmo: DFS Iterativo com Stack");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();
        
        // Descrição: DFS (Depth-First Search) Iterativo usa um Stack para explorar todos os nós de um grafo
        // sem usar recursão. Explora profundamente um caminho antes de voltar e explorar outro.

        Console.WriteLine("Exemplo de DFS Iterativo em um grafo:");
        Console.WriteLine();
        
        // Criar um grafo simples usando lista de adjacência
        Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { 2, 3 } },
            { 2, new List<int> { 4, 5 } },
            { 3, new List<int> { 6 } },
            { 4, new List<int>() },
            { 5, new List<int>() },
            { 6, new List<int> { 7 } },
            { 7, new List<int>() }
        };
        
        Console.WriteLine("Grafo:");
        Console.WriteLine("  1");
        Console.WriteLine(" / \\");
        Console.WriteLine("2   3");
        Console.WriteLine("/ \\  \\");
        Console.WriteLine("4  5  6");
        Console.WriteLine("      |");
        Console.WriteLine("      7");
        Console.WriteLine();
        
        List<int> dfsResult = DFSIterativeHelper(graph, 1);
        Console.WriteLine($"Ordem de visita (DFS Iterativo começando do nó 1): [{string.Join(", ", dfsResult)}]");
        Console.WriteLine();
        Console.WriteLine("Explicação do algoritmo:");
        Console.WriteLine("- Usa um Stack para armazenar os nós a serem visitados");
        Console.WriteLine("- Começa pelo nó inicial e o coloca no Stack");
        Console.WriteLine("- Enquanto o Stack não está vazio:");
        Console.WriteLine("  * Remove um nó do topo do Stack");
        Console.WriteLine("  * Se não foi visitado, marca como visitado");
        Console.WriteLine("  * Adiciona todos os vizinhos não visitados ao Stack");
        Console.WriteLine("- Complexidade: O(V + E) onde V é número de vértices e E é número de arestas");

        List<int> DFSIterativeHelper(Dictionary<int, List<int>> graph, int start)
        {
            List<int> visited = new List<int>();
            HashSet<int> visitedSet = new HashSet<int>();
            Stack<int> stack = new Stack<int>();
            
            stack.Push(start);
            
            while (stack.Count > 0)
            {
                int node = stack.Pop();
                
                if (!visitedSet.Contains(node))
                {
                    visitedSet.Add(node);
                    visited.Add(node);
                    
                    // Adiciona os vizinhos ao stack em ordem reversa para manter a ordem correta
                    if (graph.ContainsKey(node))
                    {
                        for (int i = graph[node].Count - 1; i >= 0; i--)
                        {
                            int neighbor = graph[node][i];
                            if (!visitedSet.Contains(neighbor))
                                stack.Push(neighbor);
                        }
                    }
                }
            }
            
            return visited;
        }
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
