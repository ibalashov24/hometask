namespace Game.Events
{
    public abstract class MoveEvent : Event
    {
        public virtual void OnMove(
            object sender,
            GameEventArgs args)
        {
            this.CallHandlers(sender, args);
        }

        public abstract void OnMove(object sender);
    }
}