namespace Game
{
    using System;

    /// <summary>
    /// A message that is sent when a game event occurs
    /// </summary>
    public class GameEventArgs : EventArgs
    {
        /// <summary>
        /// Delta of the column position
        /// </summary>
        public int ColDelta
        {
            get;
            private set;
        }

        /// <summary>
        /// Delta of the row position
        /// </summary>
        public int RowDelta
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates new instance of GameEventArgs
        /// </summary>
        /// <param name="deltaCol">Delta of the column position</param>
        /// <param name="deltaRow">Delta of the row position</param>
        public GameEventArgs(int deltaCol, int deltaRow)
        {
            this.ColDelta = deltaCol;
            this.RowDelta = deltaRow;
        }
    }
}