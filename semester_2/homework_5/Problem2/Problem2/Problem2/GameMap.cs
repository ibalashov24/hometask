namespace Game.Map
{
    using System;
    using System.IO;

    /// <summary>
    /// The map (board) of the game
    /// </summary>
    public class GameMap : IMap
    {
        /// <summary>
        /// 
        /// </summary>
        private CellType[,] board;

        /// <summary>
        /// Gets the map width
        /// </summary>
        public int Width
        {
            get => board.GetLength(1);
        }

        /// <summary>
        /// Gets the map height
        /// </summary>
        public int Height
        {
            get => board.GetLength(0);
        }

        /// <summary>
        /// Initializes map
        /// </summary>
        /// <param name="inputStream">Stream to read the map</param>
        /// <param name="mapWidth">Width of the map in cells</param>
        /// <param name="mapHeight">Height of the map in cells</param>
        public GameMap(
            StreamReader inputStream, 
            int mapWidth,
            int mapHeight)
        {
            board = new CellType[mapHeight, mapWidth];

            for (int i = 0; i < mapHeight; ++i)
            {
                for (int j = 0; j < mapWidth; ++j)
                {
                    var currentCell = inputStream.Read();

                    switch (currentCell)
                    {
                        case '.':
                            board[i, j] = CellType.Free;
                            break;
                        case '#':
                            board[i, j] = CellType.Border;
                            break;
                        default:
                            throw new Exception.InvalidCellTypeInMap(
                                currentCell.ToString());
                    }
                }

                // Allows user to make comments in the map file
                inputStream.ReadLine();
            }
        }

        /// <summary>
        /// Returns type of the map cell in given position
        /// </summary>
        /// <param name="row">Map row</param>
        /// <param name="col">Map col</param>
        /// <returns>Cell[row, col] type</returns>
        /// <seealso cref="CellType"/>
        public CellType GetCell(int row, int col)
        {
            if (row >= this.Height || row < 0 ||
                col >= this.Width || col < 0)
            {
                return CellType.NotExist;
            }

            return this.board[row, col];
        }
    }
}