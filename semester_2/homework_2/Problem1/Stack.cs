namespace StackStuff
{
    using System;

    public class Stack<T> : IStack<T>
    {
        // Size of the stack
        private int size;

        // Reference to the top element of the stack
        private StackElement top;

        // Checks if stack is empty
        public bool IsEmpty() => this.size == 0;

        // Returns top element of the stack (and deletes it)
        public T Pop()
        {
            if (this.IsEmpty())
            {
                throw new InvalidOperationException("Stack is empty!!!");
            }
            
            var returnValue = this.top;

            this.top = this.top.Next;
            --this.size;

            return returnValue.Value;
        }

        // Inserts new element to the stack
        public void Push(T value)
        {
            this.top = new StackElement(value, this.top);
            ++this.size;
        }

        private class StackElement
        {
            public readonly T Value;
            public readonly StackElement Next;

            public StackElement(T value, StackElement next)
            {
                this.Value = value;
                this.Next = next;
            }
        }
    }
}