using System;

namespace ArraySorting
{
    class Program
    {
        public static void Main()
        {
            Console.Write("Enter array (<number1> <number2> ... <numberN>): ");
            int[] inputArray = GetInput();

            Sort(ref inputArray);

            Console.WriteLine("Sorted array:");
            foreach (var element in inputArray)
            {
                Console.Write(element);
                Console.Write(' ');
            }

            Console.WriteLine();
        }

        private static void Swap(ref int a, ref int b)
        {
            var t = a;
            a = b;
            b = t;
        }

        private static void Sort(ref int[] array)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                for (int j = i; j > 0 && array[j - 1] > array[j]; --j)
                {
                    Swap(ref array[j - 1], ref array[j]);
                }
            }
        }

        private static int[] GetInput()
        {
            var inputStrings = Console.ReadLine().Split(' ');

            int[] result = new int[inputStrings.Length];
            for (int i = 0; i < inputStrings.Length; ++i)
            {
                result[i] = int.Parse(inputStrings[i]);
            }

            return result;
        }
    }
}