namespace HashTableStuff
{
    internal interface IHashTable<KeyType, ValueType>
    {
        // Allows to access hash table
        ValueType this[KeyType key] { get; set; }

        // Adds (or modify) element of the hash table
        void Add(KeyType key, ValueType value);

        // Get hash table's fill factor
        double GetFillFactor();

        // Check if element with given key is in table
        bool IsInTable(KeyType key);

        // Erases element with given key (if exists)
        void Erase(KeyType key);
    }
}