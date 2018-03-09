using System;
using System.IO;

namespace Problem1
{
    class Program
    {
        static void Main(string[] args)
        {
            var expression = ReadStringFromFile();

            var tree = new Tree.ExpressionTree(expression);

            Console.Write("Calculated value: ");
            Console.WriteLine(tree.Value);

            Console.Write("Tree representation: ");
            Console.WriteLine(tree.ToString());

            Console.Write("Infix representation: ");
            Console.WriteLine(tree.GetInfixRepresentation());
        }

        /// <summary>
        /// Reads expression tree from the file
        /// </summary>
        /// <returns>Expression string</returns>
        public static string ReadStringFromFile()
        {
            using (var stream = new StreamReader("input.txt"))
            {
                return stream.ReadLine();
            }
        }
    }
}
