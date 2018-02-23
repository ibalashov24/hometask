namespace StackStuff
{
    internal interface IStack<T>
    {
        // Inserts element to the stack
        void Push(T value);

        // Returns top element of the stack and erases it
        T Pop();

        // Checks if stack is empty
        bool IsEmpty();
    }
}