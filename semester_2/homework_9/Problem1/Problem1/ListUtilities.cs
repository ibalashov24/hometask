namespace Utility
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides some utilities for List generic
    /// </summary>
    public static class ListUtilities
    {
        /// <summary>
        /// Constructs new list from given list's elements which satisfy predicate
        /// </summary>
        /// <typeparam name="T">List element type</typeparam>
        /// <param name="list">Input sequence</param>
        /// <param name="predicate">Predicate to be checked</param>
        /// <returns>Filtered list</returns>
        public static IEnumerable<T> Filter<T>(IEnumerable<T> list, Predicate<T> predicate)
        {
            foreach (var element in list)
            {
                if (predicate(element))
                {
                    yield return element;
                }
            }
        }
    }
}