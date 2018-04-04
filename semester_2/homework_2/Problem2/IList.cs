namespace ListStuff
{
    // Interface for simple list
    internal interface IList<T>
    {
        // Returns size of the list
        int Size();

        // Inserts new element 'value' to the positition 'position'
        void Insert(T insertValue, int position);

        // Erases element from position 'position'
        void Erase(int deletePosition);

        // Erases all content in the list
        void Clean();

        // Checks if list is empty
        bool IsEmpty();
    }
}