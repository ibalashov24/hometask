namespace HashTableStuff
{
    internal interface IHashTable<TKey, TValue>
    {
        // Allows to access hash table
        TValue this[TKey key] { get; set; }

        // Adds (or modify) element of the hash table
        void Add(TKey key, TValue value);

        // Get hash table's fill factor
        double GetFillFactor();

        // Check if element with given key is in table
        bool IsInTable(TKey key);

        // Erases element with given key (if exists)
        void Erase(TKey key);
    }
}