namespace Problem2.Tests
{
    using System;

    /// <summary>
    /// Special dummy "loop" to trigger events manually
    /// </summary>
    public class ManualEventCaller : Game.EventLoop
    {
        public void MoveToRight() => 
            this.RightMove.CheckEventOccurrence(ConsoleKey.RightArrow);
        public void MoveToTop() =>
            this.UpMove.CheckEventOccurrence(ConsoleKey.UpArrow);
        public void MoveToBottom() =>
            this.DownMove.CheckEventOccurrence(ConsoleKey.DownArrow);
        public void MoveToLeft() =>
            this.LeftMove.CheckEventOccurrence(ConsoleKey.LeftArrow);
    }
}