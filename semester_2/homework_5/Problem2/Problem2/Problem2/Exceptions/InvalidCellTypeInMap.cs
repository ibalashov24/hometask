namespace Game.Map.Exception
{
    /// <summary>
    /// An exception that is thrown when an invalid type of 
    /// cell is detected when working with a map
    /// </summary>
    public class InvalidCellTypeInMap : System.Exception
    {
        public InvalidCellTypeInMap(string message) : base(message)
        { }
    }
}