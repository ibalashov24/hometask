namespace ListStuff
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Implements a simple list <see cref="IList{T}"/>
    /// </summary>
    /// <typeparam name="T">Type of elements in the list</typeparam>
    public class List<T> : System.Collections.Generic.IList<T>
    {
        // Reference to the first element
        protected ListElement listContent;
        protected int size;

        /// <summary>
        /// List is not read-only
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Returns size of the list
        /// </summary>
        /// <returns>Size of the list</returns>
        public int Count => this.size;

        /// <summary>
        /// Gets or sets element value by index
        /// </summary>
        /// <param name="i">Element index (0..size - 1)</param>
        /// <returns>Reference to the element</returns>
        public T this[int i]
        {
            get
            {
                if (i < 0 || i >= this.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                return this.GetElementByIndex(i).Value;
            }

            set
            {
                if (i < 0 || i >= this.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                this.GetElementByIndex(i).Value = value;
            }
        }

        /// <summary>
        /// Adds an item to the beginning of the list
        /// </summary>
        /// <param name="value">New element value</param>
        public void Add(T value) => this.Insert(0, value);

        /// <summary>
        /// Erases all content in the list
        /// </summary>
        public void Clear()
        {
            this.listContent = null;
            this.size = 0;
        }

        /// <summary>
        /// Determines whether the list contains a specific value.
        /// </summary>
        /// <param name="value">Value to find</param>
        /// <returns>True if given value is in the list</returns>
        public bool Contains(T value) => this.IndexOf(value) != -1;            

        /// <summary>
        /// Copies the elements of the list to an Array, 
        /// starting at a particular Array index.
        /// </summary>
        /// <param name="destination">
        /// The one-dimensional Array that is the destination 
        /// of the elements copied from the list. 
        /// </param>
        /// <param name="beginIndex">
        /// The zero-based index in array at which copying begins.
        /// </param>
        public void CopyTo(T[] destination, int beginIndex)
        {
            int currentIndex = 0;
            foreach (var element in this)
            {
                destination[beginIndex + currentIndex] = element;
                ++currentIndex;
            }
        }

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
        /// Determines the index of a specific item in the list
        /// </summary>
        /// <param name="item">Object to locate</param>
        /// <returns>
        /// The index of item if found in the list; 
        /// otherwise, -1.</returns>
        public int IndexOf(T item)
        {
            int result = 0;
            foreach (var element in this)
            {
                if (element.Equals(item))
                {
                    return result;
                }

                ++result;
            }

            return -1;
        }

        /// <summary>
        /// Inserts an item to the list at the specified index.
        /// </summary>
        /// <param name="insertValue">Value to insert</param>
        /// <param name="position">Position at which value will be placed</param>
        public virtual void Insert(int position, T insertValue)
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
                var previousElement = GetElementByIndex(position - 1);   

                newElement.Next = previousElement.Next;
                previousElement.Next = newElement;
            }

            ++this.size;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the list
        /// </summary>
        /// <param name="item">The object to remove</param>
        /// <returns>True if <c>item</c> was successfully removed</returns>
        public bool Remove(T item)
        {
            var itemPosition = this.IndexOf(item);

            if (itemPosition == -1)
            {
                return false;
            }

            this.RemoveAt(itemPosition);
            return true;
        }

        /// <summary>
        /// Removes the list item at the specified index.
        /// </summary>
        /// <param name="deletePosition">
        /// The zero-based index of the item to remove.
        /// </param>
        public void RemoveAt(int deletePosition)
        {
            if (deletePosition < 0 || deletePosition >= this.size)
            {
                throw new Exception.ElementDoesNotExistException(
                    $"Argument must be in [0..{this.size})");
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
        /// Checks if list is empty
        /// </summary>
        /// <returns>True if list is not empty</returns>
        public bool IsEmpty() => this.size == 0;

        

        /// <summary>
        /// Returns reference to the list element by index.
        /// </summary>
        /// <param name="index">Index of the element</param>
        /// <returns>
        /// Reference to list[index].
        /// Returns null if index == [list size]
        /// </returns>
        private ListElement GetElementByIndex(int index)
        {
            if (index < 0 || index > this.size)
            {
                throw new ArgumentException($"Argument must be in [0..{this.size - 1}]");
            }

            var result = this.listContent;
            for (int i = 0; i < index; ++i)
            {
                result = result.Next;
            }

            return result;
        }

        /// <summary>
        /// Single element of the list
        /// </summary>
        protected class ListElement
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
        protected class ListEnumerator : IEnumerator<T>
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