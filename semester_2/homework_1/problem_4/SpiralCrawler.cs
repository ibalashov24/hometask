using System;

namespace MatrixCrawler
{
    class SpiralCrawler
    {
        public static void Main()
        {
            Console.Write("Enter size of matrix (N): ");
            var n = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter matrix with size {0}x{0}:", n);
            var matrix = GetInput(n);

            Crawl(matrix);
        }

        private static void Crawl(int[,] matrix)
        {
            Shift[] shifts = new Shift[4] {
                    new Shift(0, 1),
                    new Shift(1, 0),
                    new Shift(0, -1),
                    new Shift(-1, 0)
            };

            Console.WriteLine("Spiral: ");

            var currentRow = matrix.GetLength(0) / 2;
            var currentCol = matrix.GetLength(1) / 2;
            var currentShiftState = 0;
            var currentStep = 0;

            while (currentCol < matrix.GetLength(0))
            {
                var currentStepSize = (currentStep / 2) + 1;
                for (int i = 0; i < currentStepSize; i++)
                {
                    Console.Write(matrix[currentRow, currentCol]);
                    Console.Write(' ');

                    currentRow += shifts[currentShiftState].RowShift;
                    currentCol += shifts[currentShiftState].ColShift;
                }

                ++currentStep;
                currentShiftState = (currentShiftState + 1) % 4;
            }
        }

        private static int[,] GetInput(int size)
        {
            int[,] result = new int[size, size];

            for (int i = 0; i < size; ++i)
            {
                var inputStrings = Console.ReadLine().Split(' ');

                for (int j = 0; j < inputStrings.Length; ++j)
                {
                    result[i, j] = int.Parse(inputStrings[j]);
                }
            }

            return result;
        }

        struct Shift
        {
            public int RowShift;
            public int ColShift;

            public Shift(int rShift, int cShift)
            {
                this.RowShift = rShift;
                this.ColShift = cShift;
            }
        }
    }
}