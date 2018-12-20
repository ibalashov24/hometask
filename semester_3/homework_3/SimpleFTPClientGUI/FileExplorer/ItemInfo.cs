namespace SimpleFTPClientGUI.FileExplorer
{
    /// <summary>
    /// Represents information about item in FileExplorer list
    /// </summary>
    public class ItemInfo
    {
        /// <summary>
        /// Item name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is item a directory
        /// </summary>
        public bool IsDirectory { get; set; }
    }
}