namespace Game.Events
{
    using System;

    /// <summary>
    /// Event that is generated when the player moves to the bottom
    /// </summary>
    internal class DownMoveEvent : MoveEvent
    {
        protected override void OnMove(object sender)
        {
            var args = new GameEventArgs(0, 1);
            base.OnMove(sender, args);
        }

        public override void CheckEventOccurrence(ConsoleKey keyInfo)
        {
            if (keyInfo == ConsoleKey.DownArrow)
            {
                OnMove(this);
            }
        }
    }
}