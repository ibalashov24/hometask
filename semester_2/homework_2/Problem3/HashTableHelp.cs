namespace Problem3
{
    /// <summary>
    /// Provides some user help stuff
    /// </summary>
    internal class HashTableHelp
    {

        /// <summary>
        /// All available messages of the program
        /// </summary>
        public static readonly HelpMessage[] Instanses = new HelpMessage[]
        {
            new HelpMessage(HelpCommands.ExitProgram, "exit program"),
            new HelpMessage(HelpCommands.InsertElement, "insert element to hash table"),
            new HelpMessage(HelpCommands.EraseElement, "erase element"),
            new HelpMessage(HelpCommands.Factor, "print fill factor"),
            new HelpMessage(HelpCommands.CheckInTable, "check if value is in table")
        };

        /// <summary>
        /// Available commands of the program
        /// </summary>
        internal enum HelpCommands
        {
            ExitProgram,
            InsertElement,
            EraseElement,
            Factor,
            CheckInTable
        }

        /// <summary>
        /// Single help message in help function
        /// </summary>
        internal class HelpMessage
        {
            public HelpMessage(HelpCommands command, string message)
            {
                this.CommandNumber = command;
                this.Message = message;
            }

            /// <summary>
            /// Gets type of the message
            /// </summary>
            public HelpCommands CommandNumber { get; }

            /// <summary>
            /// Gets message text
            /// </summary>
            public string Message { get; }
        }
    }
}