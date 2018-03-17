namespace Game.Events
{
    internal class UpMoveEvent : MoveEvent
    {
        public override void OnMove(object sender)
        {
            var args = new GameEventArgs(0, -1);
            base.OnMove(sender, args);
        }
    }
}