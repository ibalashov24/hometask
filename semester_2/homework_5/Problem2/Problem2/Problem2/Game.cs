namespace Game
{
    using System;
    using Map;

    /// <summary>
    /// Class with all game functionality
    /// You mustn't have more than 1 instance of Game in one application
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Underlay of the game map
        /// </summary>
        /// <seealso cref="GameMap"/>
        private GameMap gameMap;

        /// <summary>
        /// Current player row in the map (not screen)
        /// </summary>
        private int currentPlayerRow;
        /// <summary>
        /// Current player column in the map (not screen)
        /// </summary>
        private int currentPlayerCol;

        /// <summary>
        /// Current row in the map of the left top corner of the screen
        /// </summary>
        private int currentScreenRow;
        /// <summary>
        /// Current column in the map of the left top corner of the screen
        /// </summary>
        private int currentScreenCol;

        /// <summary>
        /// Widget that shows position of the player
        /// </summary>
        private CurrentCoordinateWidget coordinateWidget;

        /// <summary>
        /// Creates new instance of Game
        /// </summary>
        /// <param name="map">Map of the game</param>
        public Game(GameMap map)
        {
            this.gameMap = map;
            this.currentPlayerCol = currentPlayerRow = 0;
            this.currentScreenCol = currentScreenRow = 0;

            this.RedrawMap();

            coordinateWidget = new CurrentCoordinateWidget();
            coordinateWidget.Display(
                this.currentPlayerCol,
                this.currentPlayerRow);
            this.ReturnCursorToServicePosition();
        }

        /// <summary>
        /// Service function for debugging
        /// </summary>
        /// <returns>Current player position (col, row)</returns>
        public (int, int) GetPlayerPosition()
        {
            return (this.currentPlayerCol, this.currentPlayerRow);
        }

        /// <summary>
        /// Registers game to given event loop
        /// </summary>
        /// <param name="loop">Loop that notifies game about events</param>
        public void Register(EventLoop loop)
        {
            loop.LeftMove.EventList += this.OnMove;
            loop.RightMove.EventList += this.OnMove;
            loop.UpMove.EventList += this.OnMove;
            loop.DownMove.EventList += this.OnMove;
        }

        /// <summary>
        /// Method that is called then MoveEvent occured
        /// </summary>
        /// <param name="sender">Notifier</param>
        /// <param name="args">Info about the event</param>
        public void OnMove(object sender, GameEventArgs args)
        {
            var newPlayerCol = this.currentPlayerCol + args.ColDelta;
            var newPlayerRow = this.currentPlayerRow + args.RowDelta;

            var oldPlayerCol = this.currentPlayerCol;
            var oldPlayerRow = this.currentPlayerRow;

            if (this.gameMap.GetCell(newPlayerRow, newPlayerCol) ==
                CellType.Free)
            {
                this.currentPlayerRow = newPlayerRow;
                this.currentPlayerCol = newPlayerCol;

                this.coordinateWidget.Display(
                    this.currentPlayerCol,
                    this.currentPlayerRow);
                this.ReturnCursorToServicePosition();
            }

            if (this.ReviewScreen())
            {
                this.RedrawMap();
            }
            else
            {
                this.RedrawCell(newPlayerRow, newPlayerCol);
                this.RedrawCell(oldPlayerRow, oldPlayerCol);
            }
        }

        /// <summary>
        /// Redraws a single cell on the screen
        /// </summary>
        /// <param name="boardRow">Row of the cell</param>
        /// <param name="boardCol">Column of the cell</param>
        private void RedrawCell(int boardRow, int boardCol)
        {

            var realScreenCol = boardCol - this.currentScreenCol;
            var realScreenRow = boardRow - this.currentScreenRow;

            if (!(realScreenRow >= 0 && realScreenCol >= 0 &&
                realScreenRow < Console.WindowHeight - 1 &&
                realScreenCol < Console.WindowWidth))
            {
                return;
            }

            Console.SetCursorPosition(
                    realScreenCol,
                    realScreenRow);

            switch (this.gameMap.GetCell(boardRow, boardCol))
            {
                case CellType.NotExist:
                case CellType.Border:
                default:
                    Console.Write('#');
                    break;
                case CellType.Free when
                        (boardCol == this.currentPlayerCol &&
                        boardRow == this.currentPlayerRow):
                    Console.Write('@');
                    break;
                case CellType.Free:
                    Console.Write('.');
                    break;
            }

            this.ReturnCursorToServicePosition();
        }

        /// <summary>
        /// Redraws the whole map on the screen
        /// </summary>
        /// <remarks>
        /// May be very slow
        /// </remarks>
        private void RedrawMap()
        {
            // Minus service line in the bottom
            for (int i = 0; i < Console.WindowHeight - 1; ++i)
            {
                for (int j = 0; j < Console.WindowWidth; ++j)
                {
                    Console.SetCursorPosition(j, i);
                    this.RedrawCell
                        (this.currentScreenRow + i,
                        this.currentScreenCol + j);
                }
            }

            this.ReturnCursorToServicePosition();
        }

        /// <summary>
        /// Sets correct displayed part of the map where player is located
        /// </summary>
        /// <returns>True if displayed part was changed, Flase if not</returns>
        private bool ReviewScreen()
        {
            var playerColOnScreen = this.currentPlayerCol - this.currentScreenCol;
            var playerRowOnScreen = this.currentPlayerRow - this.currentScreenRow;

            if (playerColOnScreen >= Console.WindowWidth)
            {
                this.currentScreenCol += Console.WindowWidth;
                return true;
            }
            else if (playerColOnScreen < 0)
            {
                this.currentScreenCol -= Console.WindowWidth;
                return true;
            }
            else if (playerRowOnScreen >= Console.WindowHeight - 1) // Minus service line
            {
                this.currentScreenRow += Console.WindowHeight - 1;
                return true;
            }
            else if (playerRowOnScreen < 0)
            {
                this.currentScreenRow -= Console.WindowHeight - 1;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Moves the cursor to a special position 
        /// where it does not irritate the player
        /// </summary>
        private void ReturnCursorToServicePosition()
        {
            Console.SetCursorPosition(
                0,
                Console.WindowHeight - 1);
        }

        /// <summary>
        /// Block that show current player location
        /// </summary>
        private class CurrentCoordinateWidget
        {
            /// <summary>
            /// Bottom right corner of the block (column)
            /// </summary>
            private int screenCornerCol;
            /// <summary>
            /// Bottom right corner of the block (row)
            /// </summary>
            private int screenCornerRow;

            /// <summary>
            /// The column number from there the last output was started
            /// </summary>
            private int lastPrintCol;

            /// <summary>
            /// Gets or sets right bottom corner column
            /// </summary>
            public int ScreenCornerCol
            {
                get => this.screenCornerCol;
                set
                {
                    if (value < 0 || value >= Console.WindowWidth + 1)
                    {
                        throw new ArgumentOutOfRangeException(
                        "Right corner col of the coordinate widget is out of range");
                    }
                    this.screenCornerCol = value;
                }
            }

            /// <summary>
            /// Gets or sets right bottom corner row
            /// </summary>
            public int ScreenCornerRow
            {
                get => this.screenCornerRow;
                set
                {
                    if (value < 0 || value >= Console.WindowHeight)
                    {
                        throw new ArgumentOutOfRangeException(
                        "Right corner row of the coordinate widget is out of range");
                    }
                    this.screenCornerRow = value;
                }
            }

            /// <summary>
            /// Initializes new instance of CurrentCoordinateWidget
            /// </summary>
            /// <param name="beginScreenCol">Column of the right corner of the widget</param>
            /// <param name="beginScreenRow">Row of the right corner of the widget</param>
            public CurrentCoordinateWidget(
                int? beginScreenCol = null,
                int? beginScreenRow = null)
            {
                this.ScreenCornerCol = beginScreenCol != null ?
                    (int)beginScreenCol : Console.WindowWidth - 1;
                this.ScreenCornerRow = beginScreenRow != null ?
                    (int)beginScreenRow : Console.WindowHeight - 1;

                this.lastPrintCol = -1;
            }

            /// <summary>
            /// Prints the player's coordinates in the service bar at the bottom of the screen
            /// </summary>
            public void Display(int playerCol, int playerRow)
            {
                if (this.lastPrintCol >= 0)
                {
                    for (int i = this.lastPrintCol; i <= this.screenCornerCol - 1; ++i)
                    {
                        Console.SetCursorPosition(i, this.screenCornerRow);
                        Console.Write(' ');
                    }
                }

                // Minimum indentation of the coordinateString from the right border
                const int minimalMargin = 10;

                var coordinateString = $"{playerCol}:{playerRow}";

                this.lastPrintCol = this.screenCornerCol - Math.Max(minimalMargin, coordinateString.Length);
                Console.SetCursorPosition(
                    this.lastPrintCol,
                    this.screenCornerRow);

                Console.Write(coordinateString);
            }
        }
    }
}