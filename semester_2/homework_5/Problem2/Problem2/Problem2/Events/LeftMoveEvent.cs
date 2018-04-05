using System;

namespace Game.Events
{
    /// <summary>
    /// Event that is generated when the player moves to the 
    /// </summary>
    internal class LeftMoveEvent : MoveEvent
    {
        protected override void OnMove(object sender)
        {
            var args = new GameEventArgs(-1, 0);
            base.OnMove(sender, args);
        }

        public override void CheckEventOccurrence(ConsoleKey keyInfo)
        {
            if (keyInfo == ConsoleKey.LeftArrow)
            {
                OnMove(this);
            }
        }
    }
}