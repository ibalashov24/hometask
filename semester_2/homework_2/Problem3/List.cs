namespace ListStuff
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Implements a simple list <see cref="IList{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of elements in the list</typeparam>
    internal class List<T> : IList<T>, IEnumerable<T>
    {
        // Reference to the first element
        private ListElement listContent;
        private int size;

        /// <summary>
        /// Returns size of the list
        /// </summary>
        /// <returns>Size of the list</returns>
        public int Size() => this.size;

        /// <summary>
        /// Inserts new element to the list
        /// </summary>
        /// <param name="insertValue">Value to insert</param>
        /// <param name="position">Position at which value will be placed</param>
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

        /// <summary>
        /// Erases element from <c>position</c>
        /// </summary>
        /// <param name="deletePosition">Position of erased element</param>
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

        /// <summary>
        /// Erases all content in the list
        /// </summary>
        public void Clean()
        {
            while (!this.IsEmpty())
            {
                this.Erase(0);
            }
        }

        /// <summary>
        /// Checks if list is empty
        /// </summary>
        /// <returns>True if list is not empty</returns>
        public bool IsEmpty() => this.size == 0;

        /// <summary>
        /// Returns enumerator for generic list
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(this.listContent);
        }

        /// <summary>
        /// Returns enumerator for the container
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Single element of the list
        /// </summary>
        private class ListElement
        {
            public ListElement(T value, ListElement nextElement = null)
            {
                this.Value = value;
                this.Next = nextElement;
            }

            /// <summary>
            /// Gets or sets next element in the list 
            /// </summary>
            public ListElement Next { get; set; }

            /// <summary>
            /// Gets or sets value of current element
            /// </summary>
            public T Value { get; set; }

        }

        /// <summary>
        /// Implements list iterator <see cref="IEnumerator{T}"/>
        /// </summary>
        private class ListEnumerator : IEnumerator<T>
        {
            private List<T>.ListElement listBegin;
            private List<T>.ListElement currentPosition;

            public ListEnumerator(List<T>.ListElement list)
            {
                this.listBegin = list;
            }

            public T Current
            {
                get => this.currentPosition.Value;
                set => this.currentPosition.Value = value;
            }

            object IEnumerator.Current
            {
                get => this.Current;
            }

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