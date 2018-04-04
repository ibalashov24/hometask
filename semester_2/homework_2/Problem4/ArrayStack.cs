namespace StackStuff
{
    using System;

    public class ArrayStack<T> : IStack<T>
    {
        // If stack is empty its size will be increased by this factor
        public const int ResizeFactor = 2;

        // Default stack size
        public const int DefaultSize = 10;

        // Stack content
        private T[] stackUnderlay;

        // Position after the top element
        private int topPosition;

        public ArrayStack(int initialSize = DefaultSize)
        {
            if (initialSize <= 0)
            {
                throw new ArgumentException("Size must be >= 1");
            }

            this.stackUnderlay = new T[initialSize];
        }

        public void Push(T value)
        {
            this.stackUnderlay[topPosition] = value;
            ++topPosition;

            if (topPosition >= this.stackUnderlay.Length)
            {
                this.IncreaseSize();
            }
        }

        public T Pop()
        {
            if (this.topPosition == 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            --this.topPosition;
            var result = this.stackUnderlay[this.topPosition];
            this.stackUnderlay[this.topPosition] = default(T);

            return result;
        }

        public bool IsEmpty() => this.topPosition == 0;

        private void IncreaseSize()
        {
            T[] newUnderlay = new T[this.stackUnderlay.Length * 2];
            this.stackUnderlay.CopyTo(newUnderlay, 0);

            this.stackUnderlay = newUnderlay;
        }
    }
}