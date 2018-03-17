namespace Game.Map
{
    /// <summary>
    /// Lists the various available map cell types
    /// </summary>
    public enum CellType
    {
        Border,
        Free,
        NotExist
    }

    /// <summary>
    /// The map (board) of the game
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Gets the map width
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the map height
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Returns type of the map cell in given position
        /// </summary>
        /// <param name="row">Map row</param>
        /// <param name="col">Map col</param>
        /// <returns>Cell[row, col] type</returns>
        /// <seealso cref="CellType"/>
        CellType GetCell(int row, int col);
    }
}