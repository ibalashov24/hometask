namespace Game
{
    using System;
    using Events;

    /// <summary>
    /// Game event loop
    /// </summary>
    public class GameEventLoop : EventLoop
    {
        /// <summary>
        /// Runs event loop
        /// </summary>
        public void Run()
        {
            while (true)
            {
                var currentInput = Console.ReadKey(true);

                this.LeftMove.CheckEventOccurrence(currentInput.Key);
                this.RightMove.CheckEventOccurrence(currentInput.Key);
                this.UpMove.CheckEventOccurrence(currentInput.Key);
                this.DownMove.CheckEventOccurrence(currentInput.Key);
            }
        }
    }
}