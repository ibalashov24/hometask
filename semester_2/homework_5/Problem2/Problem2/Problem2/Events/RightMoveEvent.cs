namespace Game.Events
{
    using System;

    /// <summary>
    /// Event that is generated when the player moves to the right
    /// </summary>
    internal class RightMoveEvent : MoveEvent
    {
        protected override void OnMove(object sender)
        {
            var args = new GameEventArgs(1, 0);
            base.OnMove(sender, args);
        }

        public override void CheckEventOccurrence(ConsoleKey keyInfo)
        {
            if (keyInfo == ConsoleKey.RightArrow)
            {
                OnMove(this);
            }
        }
    }
}