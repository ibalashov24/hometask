namespace ListStuff
{
    public interface IList<T>
    {
        /// <summary>
        /// Returns size of the list
        /// </summary>
        /// <returns>Size of the list</returns>
        int Size();

        /// <summary>
        /// Inserts new element to the list
        /// </summary>
        /// <param name="insertValue">Value to insert</param>
        /// <param name="position">Position at which value will be placed</param>
        void Insert(T insertValue, int position);

        /// <summary>
        /// Erases element from position 'position'
        /// </summary>
        /// <param name="deletePosition">Position of erased element</param>
        void Erase(int deletePosition);

        /// <summary>
        /// Erases all content in the list
        /// </summary>
        void Clean();

        /// <summary>
        /// Checks if list is empty
        /// </summary>
        /// <returns>True if list is not empty</returns>
        bool IsEmpty();
    }
}