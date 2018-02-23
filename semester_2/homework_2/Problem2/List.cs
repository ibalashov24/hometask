namespace ListStuff
{
    using System.Collections;

    internal class List<T> : IList<T>, IEnumerable
    {
        // Reference to the first element
        private ListElement listContent;
        private int size;

        public List()
        {
            this.size = 0;
            this.listContent = null;
        }

        public int Size()
        {
            return this.size;
        }

        public void Insert(T insertValue, int position)
        {
            if (position < 0 || position > this.size)
            {
                return;
            }

            var newElement = new ListElement(insertValue, null);
            if (position == 0)
            {
                newElement.Next = this.listContent;
                this.listContent = newElement;
            }
            else
            {
                var previousElement = this.listContent;
                for (int i = 0; i < position - 2; ++i)
                {
                    previousElement = previousElement.Next;
                }

                newElement.Next = previousElement.Next;
                previousElement.Next = newElement;
            }

            ++this.size;
        }

        public void Erase(int deletePosition)
        {
            if (deletePosition < 0 || deletePosition >= this.size)
            {
                return;
            }

            if (deletePosition == 0)
            {
                this.listContent = this.listContent.Next;
                return;
            }
            else
            {
                var previousElement = this.listContent;
                for (int i = 0; i < deletePosition - 1; ++i)
                {
                    previousElement = previousElement.Next;
                }

                previousElement.Next = previousElement.Next.Next;
            }

            --this.size;
        }

        public void Clean()
        {
            while (!this.IsEmpty())
            {
                this.Erase(0);
            }
        }

        public bool IsEmpty()
        {
            return this.size == 0;
        }

        public IEnumerator GetEnumerator()
        {
            return new ListEnumerator<T>(this.listContent);
        }

        internal class ListElement
        {
            public ListElement Next;
            public T Value;

            public ListElement(T value, ListElement nextElement = null)
            {
                this.Value = value;
                this.Next = nextElement;
            }
        }
    }
}