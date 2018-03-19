namespace Game
{
    using System;
    using Events;

    /// <summary>
    /// Game event loop
    /// </summary>
    public class EventLoop
    {
        /// <summary>
        /// Event "player moved cursor to the left"
        /// </summary>
        public MoveEvent LeftMove { get; } = new LeftMoveEvent();

        /// <summary>
        /// Event "player moved cursor to the right"
        /// </summary>
        public MoveEvent RightMove { get; } = new RightMoveEvent();

        /// <summary>
        /// Event "player moved curor up"
        /// </summary>
        public MoveEvent UpMove { get; } = new UpMoveEvent();

        /// <summary>
        /// Event "player moved cursor down"
        /// </summary>
        public MoveEvent DownMove { get; } = new DownMoveEvent();

        /// <summary>
        /// Runs event loop
        /// </summary>
        public void Run()
        {
            while (true)
            {
                var currentInput = Console.ReadKey(true);

                switch (currentInput.Key)
                {
                    case ConsoleKey.LeftArrow:
                        this.LeftMove.OnMove(this);
                        break;
                    case ConsoleKey.RightArrow:
                        this.RightMove.OnMove(this);
                        break;
                    case ConsoleKey.UpArrow:
                        this.UpMove.OnMove(this);
                        break;
                    case ConsoleKey.DownArrow:
                        this.DownMove.OnMove(this);
                        break;
                }
            }
        }
    }
}