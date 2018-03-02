namespace HashTableStuff
{
    /// <summary>
    /// Implements simple hash table
    /// </summary>
    /// <typeparam name="TKey">Element key type in the table</typeparam>
    /// <typeparam name="TValue">Element value type in the table</typeparam>
    internal interface IHashTable<TKey, TValue>
    {
        // Gets hash table's fill factor
        double FillFactor { get; }

        /// <summary>
        /// Allows to access hash table
        /// </summary>
        /// <param name="key">Identifier of the element</param>
        /// <returns>Value of set element with key <c>key</c></returns>
        TValue this[TKey key] { get; set; }

        /// <summary>
        /// Adds (or modifies) element of the hash table
        /// </summary>
        /// <param name="key">Identifier of the element</param>
        /// <param name="value">Value of the new element</param>
        void Add(TKey key, TValue value);

        /// <summary>
        /// Check if element with given key is in table
        /// </summary>
        /// <param name="key">Identifier of the element</param>
        /// <returns>True if element with given key is in table</returns>
        bool IsInTable(TKey key);

        /// <summary>
        /// Erases element with given key (if exists)
        /// </summary>
        /// <param name="key">Identifier of the element</param>
        void Erase(TKey key);
    }
}