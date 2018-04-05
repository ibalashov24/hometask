namespace Game.Events
{
    using System;

    /// <summary>
    /// Base type for events
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Contains all the handlers for the current event
        /// </summary>
        public event EventHandler<GameEventArgs> Handlers
            = (sender, args) => { };

        /// <summary>
        /// Executes all the handlers of the current event
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="args">Event parameters</param>
        protected void CallHandlers(object sender, GameEventArgs args)
        {
            Handlers(sender, args);
        }
    }
}