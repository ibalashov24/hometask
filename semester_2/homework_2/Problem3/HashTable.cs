namespace HashTableStuff
{
    using System;

    internal class HashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        // New bucket count after resize == oldBucketCount * ResizeFactor
        private static readonly int ResizeFactor = 2;

        // The hash table resizes when fill factor >= MaxFillFactor
        private static readonly double MaxFillFactor = 1.0;

        // Size of the table
        private int size;

        // Array of buckets of the hash table
        private ListStuff.List<TableElement>[] buckets;

        public HashTable(int allocatedSize = 1)
        {
            if (allocatedSize <= 0)
            {
                throw new ArgumentException("Size must be >= 1");
            }

            this.size = 0;
            this.buckets = new ListStuff.List<TableElement>[allocatedSize];

            for (int i = 0; i < this.buckets.Length; ++i)
            {
                this.buckets[i] = new ListStuff.List<TableElement>();
            }
        }

        // Allows to access hash table
        public TValue this[TKey key]
        {
            get
            {
                var element = this.FindElement(key);
                if (element != null)
                {
                    return element.Value;
                }

                throw new ArgumentOutOfRangeException("Invalid key");
            }

            set
            {
                if (!this.IsInTable(key))
                {
                    this.Add(key, value);
                }
                else
                {
                    this.FindElement(key).Value = value;
                }
            }
        }

        // Adds (or modify) element of the hash table
        public void Add(TKey key, TValue value)
        {
            var element = this.FindElement(key);
            if (element != null)
            {
                element.Value = value;
                return;
            }

            var keyHash = GetBucketNumber(this.buckets, key);
            this.buckets[keyHash].Insert(new TableElement { Key = key, Value = value }, 0);

            ++this.size;

            this.OptimizeTableSize();
        }

        // Get hash table's fill factor
        public double GetFillFactor()
        {
            return (double)this.size / this.buckets.Length;
        }

        // Check if element with given key is in table
        public bool IsInTable(TKey key)
        {
            var element = this.FindElement(key);

            return element != null;
        }

        // Erases element with given key (if exists)
        public void Erase(TKey key)
        {
            if (!this.IsInTable(key))
            {
                return;
            }

            var keyHash = GetBucketNumber(this.buckets, key);

            var position = 0;
            foreach (TableElement element in this.buckets[keyHash])
            {
                if (element.Key.Equals(key))
                {
                    break;
                }

                ++position;
            }

            this.buckets[keyHash].Erase(position);

            --this.size;
        }

        // Returns bucket number for given key (based on hash)
        private static int GetBucketNumber(ListStuff.List<TableElement>[] buckets, TKey key) =>
            Math.Abs(key.GetHashCode()) % buckets.Length;

        // Returns reference to the element with given key (or 'null' if not found)
        private TableElement FindElement(TKey key)
        {
            var keyHash = GetBucketNumber(this.buckets, key);

            foreach (TableElement element in this.buckets[keyHash])
            {
                if (element.Key.Equals(key))
                {
                    return element;
                }
            }

            return null;
        }

        // Increases the number of buckets in the table if it is full
        private void OptimizeTableSize()
        {
            if (this.GetFillFactor() > HashTable<TKey, TValue>.MaxFillFactor)
            {
                var newBucketCount = this.buckets.Length * HashTable<TKey, TValue>.ResizeFactor;

                var newBuckets = new ListStuff.List<TableElement>[newBucketCount];
                for (int i = 0; i < newBucketCount; ++i)
                {
                    newBuckets[i] = new ListStuff.List<TableElement>();
                }

                foreach (var bucket in this.buckets)
                {
                    foreach (TableElement element in bucket)
                    {
                        var newHash = GetBucketNumber(newBuckets, element.Key);
                        newBuckets[newHash].Insert(element, 0);
                    }
                }

                this.buckets = newBuckets;
            }
        }

        // Single element in bucket
        private class TableElement
        {
            public TKey Key { get; set; }

            public TValue Value { get; set; }
        }
    }
}