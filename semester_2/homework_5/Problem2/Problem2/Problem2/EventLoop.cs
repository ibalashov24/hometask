namespace Game
{
    using System;
    using Events;

    /// <summary>
    /// Game event loop
    /// </summary>
    public class EventLoop
    {
        public MoveEvent LeftMove { get; } = new LeftMoveEvent();
        public MoveEvent RightMove { get; } = new RightMoveEvent();
        public MoveEvent UpMove { get; } = new UpMoveEvent();
        public MoveEvent DownMove { get; } = new DownMoveEvent();
        
        public EventLoop()
        { }

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