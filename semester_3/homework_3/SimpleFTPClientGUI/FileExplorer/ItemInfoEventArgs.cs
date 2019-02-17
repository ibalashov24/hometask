using System;

namespace SimpleFTPClientGUI.FileExplorer
{
    /// <summary>
    /// Represents event args for ItemRequested event
    /// </summary>
    public class ItemInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new intance of ItemInfoEventArgs
        /// </summary>
        /// <param name="itemInfo">Item info</param>
        public ItemInfoEventArgs(ItemInfo itemInfo)
        {
            this.ItemInfo = itemInfo;
        }

        /// <summary>
        /// Info about item
        /// </summary>
        public ItemInfo ItemInfo { get; }
    }
}
