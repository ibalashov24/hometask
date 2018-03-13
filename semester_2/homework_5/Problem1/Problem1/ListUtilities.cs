namespace Utility
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides some utilities for List generic
    /// </summary>
    static class ListUtilities
    {
        /// <summary>
        /// Applies function for each element of the list
        /// </summary>
        /// <typeparam name="T">List element type</typeparam>
        /// <param name="list">Input list</param>
        /// <param name="handler">Function to be applied</param>
        /// <returns>Result list</returns>
        public static List<T> Map<T>(List<T> list, Func<T, T> handler)
        {
            List<T> result = new List<T>();
            foreach (var element in list)
            {
                result.Add(handler(element));
            }

            return result;
        }

        /// <summary>
        /// Constructs new list from given list's elements which satisfy predicate
        /// </summary>
        /// <typeparam name="T">List element type</typeparam>
        /// <param name="list">Input list</param>
        /// <param name="predicate">Predicate to be checked</param>
        /// <returns>Filtered list</returns>
        public static List<T> Filter<T>(List<T> list, Predicate<T> predicate)
        {
            List<T> result = new List<T>();
            foreach (var element in list)
            {
                if (predicate(element))
                {
                    result.Add(element);
                }
            }

            return result;
        }

        /// <summary>
        /// Accumulates elements of the list using given accumulator
        /// </summary>
        /// <typeparam name="T">List element type</typeparam>
        /// <param name="list">Input list</param>
        /// <param name="beginValue">The first value</param>
        /// <param name="accumulator">Function which accumulates acc and current element</param>
        /// <returns>The result of accumulation</returns>
        public static T Fold<T>(List<T> list, T beginValue, Func<T, T, T> accumulator)
        {
            T result = beginValue;
            foreach (var element in list)
            {
                result = accumulator(result, element);
            }

            return result;
        }
    }
}