namespace Game.Events
{
    internal class RightMoveEvent : MoveEvent
    {
        public override void OnMove(object sender)
        {
            var args = new GameEventArgs(1, 0);
            base.OnMove(sender, args);
        }
    }
}