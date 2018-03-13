namespace Program
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter list size: ");
            var listSize = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter list (int): ");
            List<int> list = new List<int>();
            for (int i = 0; i < listSize; ++i)
            {
                Console.Write($"{i}: ");
                list.Add(int.Parse(Console.ReadLine()));
            }

            Console.Write("Enter begin value: ");
            var beginValue = int.Parse(Console.ReadLine());

            Console.WriteLine("Result:");

            Console.WriteLine("Map(): ");
            foreach (var element in Utility.ListUtilities.Map<int>(list, 
                (current) => current * 10))
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine();

            Console.WriteLine("Filter(): ");
            foreach (var element in Utility.ListUtilities.Filter<int>(list,
                (current) => current % 7 == 0))
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine();

            Console.WriteLine("Fold(): ");
            Console.WriteLine(Utility.ListUtilities.Fold<int>(list, beginValue,
                (acc, elem) => acc * elem));
        }
    }
}