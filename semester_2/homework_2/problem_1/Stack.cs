namespace StackStuff
{
    public class Stack<T> : IStack<T>
    {
        private int size = 0;
        private StackElement top = default(StackElement);

        public Stack()
        {
        }

        public bool IsEmpty()
        {
            return this.size == 0;
        }

        public T Pop()
        {
            if (this.IsEmpty())
            {
                // TODO: Replace with exception
                return default(T);
            }
            else
            {
                var returnValue = this.top;

                this.top = this.top.Next;
                --this.size;

                return returnValue.Value;
            }
        }

        public void Push(T value)
        {
            this.top = new StackElement(value, this.top);
            ++this.size;
        }

        private class StackElement
        {
            public T Value;
            public StackElement Next;

            public StackElement(T value, StackElement next)
            {
                this.Value = value;
                this.Next = next;
            }
        }
    }
}