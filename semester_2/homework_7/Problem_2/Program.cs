using System;

namespace TestOfSet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PrintHelp();
            var treap = new CustomSet.Treap<int>();

           /* Console.WriteLine("15".GetHashCode());
		Console.WriteLine("20".GetHashCode());
		Console.WriteLine("1252".GetHashCode());*/

            while (true)
            {
                Console.Write("Enter command: ");
                if (!int.TryParse(Console.ReadLine(), out int currentCommand))
                {
                    PrintHelp();
                    continue;
                }

                switch (currentCommand)
                {
                    case 0:
                        Console.WriteLine("Good bye!");
                        return;
                    case 1:
                        Console.Write("Enter value to add: ");
                        var newValue = int.Parse(Console.ReadLine());

                        if (treap.Add(newValue))
                        {
                            Console.WriteLine("Success!");
                        }
                        else
                        {
                            Console.WriteLine("Failure!");
                        }

                        break;
                    case 2:
                        Console.Write("Enter value to remove: ");
                        var removeValue = int.Parse(Console.ReadLine());

                        if (treap.Remove(removeValue))
                        {
                            Console.WriteLine("Success");
                        }
                        else
                        {
                            Console.WriteLine("Failure (value is not in set?)");
                        }

                        break;
                    case 3:
                        if (treap.Count == 0)
                        {
                            Console.WriteLine("Nothing");
                        }
                        else
                        {
                            foreach (var element in treap)
                            {
                                Console.Write($"{element} ");
                            }
                            Console.WriteLine();
                        }
                        break;
                    default:
                        PrintHelp();
                        break;
                }
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Enter 0 to exit program");
            Console.WriteLine("Enter 1 to add new value");
            Console.WriteLine("Enter 2 to remove value");
            Console.WriteLine("Enter 3 to print set elements");
        }
    }
}