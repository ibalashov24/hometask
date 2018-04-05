namespace Game.Events
{
    using System;

    /// <summary>
    /// Base class for events connected with movement
    /// </summary>
    public abstract class MoveEvent : Event
    {
        /// <summary>
        /// Initializes new instance of MoveEvent's child
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="args">Info about the event</param>
        public virtual void OnMove(
            object sender,
            GameEventArgs args)
        {
            this.CallHandlers(sender, args);
        }

        /// <summary>
        /// Calls event's handers
        /// </summary>
        /// <param name="sender"></param>
        protected abstract void OnMove(object sender);

        /// <summary>
        /// Checks if an event has occurred, and if so, processes it
        /// </summary>
        /// <param name="keyInfo">Info about pressed key</param>
        public abstract void CheckEventOccurrence(ConsoleKey keyInfo);
    }
}