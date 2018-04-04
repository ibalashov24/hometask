namespace ListStuff
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    internal class List<T> : IList<T>, IEnumerable<T>
    {
        // Reference to the first element
        private ListElement listContent;
        private int size;

        public int Size() => this.size;

        public void Insert(T insertValue, int position)
        {
            if (position < 0 || position > this.size)
            {
                throw new ArgumentException($"Argument must be in [0..{this.size}]");
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
                throw new ArgumentException($"Argument must be in [0..{this.size})");
            }

            if (deletePosition == 0)
            {
                this.listContent = this.listContent.Next;
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

        public bool IsEmpty() => this.size == 0;

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(this.listContent);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ListElement
        {
            public ListElement Next { get; set; }
            public T Value { get; set; }

            public ListElement(T value, ListElement nextElement)
            {
                this.Value = value;
                this.Next = nextElement;
            }
        }

        private class ListEnumerator : IEnumerator<T>
        {
            private ListElement listBegin;
            private ListElement currentPosition;

            public ListEnumerator(ListElement list)
            {
                this.listBegin = list;
            }

            public T Current
            {
                get => this.currentPosition.Value;
                set => this.currentPosition.Value = value;
            }

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (this.listBegin == null ||
                    (this.currentPosition != null && this.currentPosition.Next == null))
                {
                    return false;
                }
                else if (this.currentPosition == null)
                {
                    this.currentPosition = this.listBegin;
                }
                else
                {
                    this.currentPosition = this.currentPosition.Next;
                }

                return true;
            }

            public void Reset()
            {
                this.currentPosition = null;
            }
        }
    }
}