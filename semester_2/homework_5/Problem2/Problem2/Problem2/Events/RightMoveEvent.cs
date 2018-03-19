namespace Game.Events
{
    /// <summary>
    /// Event that is generated when the player moves to the right
    /// </summary>
    internal class RightMoveEvent : MoveEvent
    {
        public override void OnMove(object sender)
        {
            var args = new GameEventArgs(1, 0);
            base.OnMove(sender, args);
        }
    }
}