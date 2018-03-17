namespace Game.Events
{
    internal class DownMoveEvent : MoveEvent
    {
        public override void OnMove(object sender)
        {
            var args = new GameEventArgs(0, 1);
            base.OnMove(sender, args);
        }
    }
}