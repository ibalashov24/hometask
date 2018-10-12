namespace SimpleFTP
{
    /// <summary>
    /// Provides some information about file
    /// </summary>
    public class FileMetaInfo
    {
    public FileMetaInfo(string name, bool isDirectory)
        {
            this.Name = name;
            this.IsDirectory = isDirectory;
        }

        public string Name { get; }

        public bool IsDirectory { get; }
    }
}
