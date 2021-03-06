﻿namespace ListStuff
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// List which has only one (or 0) entry of element with each value
    /// </summary>
    /// <typeparam name="T">Element type</typeparam>
    public class UniqueList<T> : List<T>, IEnumerable<T>
    {
        /// <summary>
        /// Checks if element with value == <c>value</c>is already in list
        /// </summary>
        /// <param name="value">Check value</param>
        /// <returns>True if element is in list</returns>
        public bool IsInList(T value)
        {
            foreach (var element in this)
            {
                if (element.Equals(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Inserts a new element with a specified value 
        /// (if it is not already in the list) to the specified position
        /// </summary>
        /// <param name="insertValue">Value to insert</param>
        /// <param name="position">Insert position</param>
        public override void Insert(T insertValue, int position)
        {
            if (this.IsInList(insertValue))
            {
                throw new Exception.AlreadyExistException("Insert value is already in list");
            }

            base.Insert(insertValue, position);
        }

        /// <summary>
        /// Implements enumerator stuff
        /// </summary>
        /// <returns>Current enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Implements enumerator stuff
        /// </summary>
        /// <returns>Current enumerator</returns>
        public new IEnumerator<T> GetEnumerator()
        {
            var currentPosition = this.listContent;
            while (currentPosition != null)
            {
                yield return currentPosition.Value;
                currentPosition = currentPosition.Next;
            }
        }

    }
}