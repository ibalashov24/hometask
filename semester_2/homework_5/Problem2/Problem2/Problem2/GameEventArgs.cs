namespace Game
{
    using System;
    
    public class GameEventArgs : EventArgs
    {
        public int ColDelta
        {
            get;
            private set;
        }

        public int RowDelta
        {
            get;
            private set;
        }

        public GameEventArgs(int deltaCol, int deltaRow)
        {
            this.ColDelta = deltaCol;
            this.RowDelta = deltaRow;
        }
    }
}