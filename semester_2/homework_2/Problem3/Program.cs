namespace Problem3
{
    using System;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var hashTable = new HashTableStuff.HashTable<string, int>();

            PrintHelp();

            while (true)
            {
                Console.Write("Enter command: ");
                var currentCommand = int.Parse(Console.ReadLine());

                if (!HashTableHelp.HelpCommands.IsDefined(typeof(HashTableHelp.HelpCommands), currentCommand))
                {
                    Console.WriteLine("Invalid command!");
                    PrintHelp();
                }
                else
                {
                    switch ((HashTableHelp.HelpCommands)currentCommand)
                    {
                        case HashTableHelp.HelpCommands.ExitProgram:
                            Console.WriteLine("Good bye!");
                            return;

                        case HashTableHelp.HelpCommands.CheckInTable:
                            Console.Write("Enter key to check: ");
                            var checkKey = Console.ReadLine();
                            Console.WriteLine(hashTable.IsInTable(checkKey) ? "In table" : "Not in table");
                            break;

                        case HashTableHelp.HelpCommands.InsertElement:
                            Console.Write("Enter key to insert (or modify): ");
                            var insertKey = Console.ReadLine();
                            Console.Write($"Enter new value of table[\"{insertKey}\"] (int): ");
                            var insertValue = int.Parse(Console.ReadLine());
                            hashTable[insertKey] = insertValue;
                            break;

                        case HashTableHelp.HelpCommands.EraseElement:
                            Console.Write("Enter key to erase: ");
                            var eraseKey = Console.ReadLine();
                            hashTable.Erase(eraseKey);
                            break;

                        case HashTableHelp.HelpCommands.Factor:
                            Console.WriteLine($"Fill factor == {hashTable.GetFillFactor()}");
                            break;

                        default:
                            PrintHelp();
                            break;
                    }
                }
            }
        }

        private static void PrintHelp()
        {
            foreach (var instance in HashTableHelp.Instanses)
            {
                Console.WriteLine($"Enter {(int)instance.CommandNumber} to {instance.Message}");
            }
        }
    }
}