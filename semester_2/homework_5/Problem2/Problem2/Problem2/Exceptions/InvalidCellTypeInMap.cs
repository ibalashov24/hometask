namespace Game.Map.Exception
{
    /// <summary>
    /// An exception that is thrown when an invalid type of 
    /// cell is detected when working with a map
    /// </summary>
    public class InvalidCellTypeInMapException : System.Exception
    {
        public InvalidCellTypeInMapException(string message) : base(message)
        { }
    }
}