namespace Game.Events
{
    /// <summary>
    /// Event that is generated when the player moves to the bottom
    /// </summary>
    internal class DownMoveEvent : MoveEvent
    {
        public override void OnMove(object sender)
        {
            var args = new GameEventArgs(0, 1);
            base.OnMove(sender, args);
        }
    }
}