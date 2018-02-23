using System;

namespace ColumnSort
{
    class Program
    {
        public static void Main()
        {
            Console.Write("Enter number of columns: ");
            var colCount = int.Parse(Console.ReadLine());
            Console.Write("Enter number of rows: ");
            var rowCount = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter matrix:");
            var matrix = ReadMatrix(colCount, rowCount);

            SortMatrix(ref matrix);

            Console.WriteLine("Result:");
            PrintMatrix(matrix);
        }

        private static int[,] ReadMatrix(int colCount, int rowCount)
        {
            int[,] result = new int[rowCount, colCount];

            for (var i = 0; i < rowCount; ++i)
            {
                var inputStrings = Console.ReadLine().Split(' ');

                for (var j = 0; j < inputStrings.Length; ++j)
                {
                    result[i, j] = int.Parse(inputStrings[j]);
                }
            }

            return result;
        }

        private static void SwapColumns(ref int[,] matrix, int columnA, int columnB)
        {
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                var t = matrix[i, columnA];
                matrix[i, columnA] = matrix[i, columnB];
                matrix[i, columnB] = t;
            }
        }

        private static void SortMatrix(ref int[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(1); ++i)
            {
                for (var j = i; j > 0 && matrix[0, j - 1] > matrix[0, j]; --j)
                {
                    SwapColumns(ref matrix, j - 1, j);
                }
            }
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); ++i)
            {
                for (var j = 0; j < matrix.GetLength(1); ++j)
                {
                    Console.Write("{0} ", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}