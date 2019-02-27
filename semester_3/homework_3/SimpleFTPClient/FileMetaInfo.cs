namespace SimpleFTP
{
    /// <summary>
    /// Provides some information about file
    /// </summary>
    public class FileMetaInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SimpleFTP.FileMetaInfo"/> class.
        /// </summary>
        /// <param name="name">Item name</param>
        /// <param name="isDirectory">True if item is directory</param>
        public FileMetaInfo(string name, bool isDirectory)
        {
            this.Name = name;
            this.IsDirectory = isDirectory;
        }

        /// <summary>
        /// Gets the name of file
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets a value indicating whether this is a directory.
        /// </summary>
        /// <value><c>true</c> if is directory; otherwise, <c>false</c>.</value>
        public bool IsDirectory { get; }
    }
}
