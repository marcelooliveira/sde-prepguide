using System;

class Program
{
	static void Main()
	{
		int[] nums = { 3, 4, -1, 1 };
		int result = FirstMissingPositive(nums);
		Console.WriteLine($"O menor inteiro positivo faltante é: {result}");
	}

	public static int FirstMissingPositive(int[] nums)
	{
		int n = nums.Length;

		for (int i = 0; i < n; i++)
		{
			// Enquanto o número atual estiver no intervalo [1, n] 
			// e não estiver na posição correta (nums[i] != nums[nums[i] - 1])
			while (nums[i] > 0 && nums[i] <= n && nums[nums[i] - 1] != nums[i])
			{
				// Troca o elemento para a posição correta
				int correctIdx = nums[i] - 1;
				int temp = nums[i];
				nums[i] = nums[correctIdx];
				nums[correctIdx] = temp;
			}
		}

		// Verifica o primeiro índice onde o valor não corresponde a i + 1
		for (int i = 0; i < n; i++)
		{
			if (nums[i] != i + 1)
			{
				return i + 1;
			}
		}

		// Se todos estiverem corretos, o próximo é n + 1
		return n + 1;
	}
}