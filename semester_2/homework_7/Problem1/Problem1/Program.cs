// Fairly borrowed this file from HT #2

namespace Problem1
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new ListStuff.List<int>();

            PrintHelp();

            while (true)
            {
                Console.Write("Enter command: ");
                if (!int.TryParse(Console.ReadLine(), out var currentCommandNumber))
                {
                    Console.WriteLine("Command must be integer!");
                    continue;
                }

                if (Enum.IsDefined(typeof(Help.HelpCommands), currentCommandNumber))
                {
                    switch ((Help.HelpCommands)currentCommandNumber)
                    {
                        case Help.HelpCommands.ExitProgram:
                            Console.WriteLine("Good bye!");
                            return;
                        case Help.HelpCommands.InsertElement:
                            try
                            {
                                Console.Write("Enter new element value: ");
                                var insertValue = int.Parse(Console.ReadLine());
                                Console.Write($"Enter position (0..{list.Count}): ");
                                var position = int.Parse(Console.ReadLine());

                                list.Insert(position, insertValue);
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("Incorrect format!!!");
                            }

                            break;
                        case Help.HelpCommands.EraseElement:
                            Console.Write($"Enter delete position (0 <= pos < {list.Count}): ");
                            var deletePosition = int.Parse(Console.ReadLine());
                            list.RemoveAt(deletePosition);
                            break;
                        case Help.HelpCommands.CleanList:
                            list.Clear();
                            Console.WriteLine("List is empty now");
                            break;
                        case Help.HelpCommands.Size:
                            Console.WriteLine($"List size == {list.Count}");
                            break;
                        case Help.HelpCommands.PrintList:
                            Console.WriteLine("List content: ");
                            PrintList(list);
                            break;
                        default:
                            PrintHelp();
                            break;
                    }
                }
                else
                {
                    PrintHelp();
                }
            }
        }

        private static void PrintHelp()
        {
            foreach (var command in Help.Instanses)
            {
                Console.WriteLine($"Print {(int)command.CommandNumber} to {command.Message}");
            }
        }

        private static void PrintList(ListStuff.List<int> list)
        {
            if (list.IsEmpty())
            {
                Console.WriteLine("Empty");
                return;
            }

            foreach (var listInstance in list)
            {
                Console.Write($"{listInstance} ");
                Console.WriteLine();
            }
        }
    }
}