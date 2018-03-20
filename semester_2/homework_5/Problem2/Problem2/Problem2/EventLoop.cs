namespace Game
{
    using System;
    using Events;

    public abstract class EventLoop
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
    }
}