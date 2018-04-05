// Fairly borrowed this file from HT #2

namespace Problem1
{
    /// <summary>
    /// Contains some user help stuff
    /// </summary>
    internal class Help
    {
        /// <summary>
        /// Descriptions for the help commands
        /// </summary>
        public static readonly HelpMessage[] Instanses =
        {
            new HelpMessage(HelpCommands.ExitProgram, "exit program"),
            new HelpMessage(HelpCommands.InsertElement, "insert element to the list"),
            new HelpMessage(HelpCommands.EraseElement, "erase element"),
            new HelpMessage(HelpCommands.CleanList, "clean list"),
            new HelpMessage(HelpCommands.Size, "print list size"),
            new HelpMessage(HelpCommands.PrintList, "print list content")
        };

        /// <summary>
        /// Available help commands
        /// </summary>
        internal enum HelpCommands
        {
            ExitProgram,
            InsertElement,
            EraseElement,
            CleanList,
            Size,
            PrintList
        }

        /// <summary>
        /// Represents single help message
        /// </summary>
        internal struct HelpMessage
        {
            public HelpCommands CommandNumber;
            public string Message;

            public HelpMessage(HelpCommands command, string message)
            {
                this.CommandNumber = command;
                this.Message = message;
            }
        }
    }
}