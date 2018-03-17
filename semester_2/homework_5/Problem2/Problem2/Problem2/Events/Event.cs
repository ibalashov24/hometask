namespace Game.Events
{
    using System;

    public abstract class Event
    {
        public event EventHandler<GameEventArgs> EventList;
        
        public Event()
        {
            this.EventList += (sender, args) => { };
        }

        protected void CallHandlers(object sender, GameEventArgs args)
        {
            EventList(sender, args);
        }
    }
}