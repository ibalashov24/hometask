namespace Game
{
    using System;
    using Map;

    public class Game
    {
        private GameMap gameMap;

        private int currentPlayerRow;
        private int currentPlayerCol;

        private int currentScreenRow;
        private int currentScreenCol;

        private CurrentCoordinateWidget coordinateWidget;

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

        public void Register(EventLoop loop)
        {
            loop.LeftMove.EventList += this.OnMove;
            loop.RightMove.EventList += this.OnMove;
            loop.UpMove.EventList += this.OnMove;
            loop.DownMove.EventList += this.OnMove;
        }

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

        private void ReturnCursorToServicePosition()
        {
            Console.SetCursorPosition(
                0,
                Console.WindowHeight - 1);
        }

        private class CurrentCoordinateWidget
        {
            private int screenCornerCol;
            private int screenCornerRow;

            private int lastPrintCol;

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