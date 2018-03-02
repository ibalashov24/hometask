namespace HashTableStuff
{
    using System;

    public class HashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        /// <summary>
        /// New bucket count after resize == <c>oldBucketCount * ResizeFactor</c>
        /// </summary>
        private const int ResizeFactor = 2;

        /// <summary>
        /// The hash table resizes when <c>fill factor >= MaxFillFactor</c>
        /// </summary>
        private const double MaxFillFactor = 1.0;

        /// <summary>
        /// Size of the table
        /// </summary>
        private int size;

        /// <summary>
        /// The hash function used by the hash table
        /// </summary>
        private HashFunctionType<TKey> hashFunction;

        /// <summary>
        /// Array of buckets of the hash table
        /// </summary>
        private ListStuff.List<TableElement>[] buckets;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashTable{TKey, TValue}"/> class
        /// </summary>
        /// <param name="hashFunction">Custom hash function
        /// (default <see cref="object.GetHashCode"/> will be used if not given)</param>
        /// <param name="allocatedSize">Hash table size in the beginning</param>
        public HashTable(
            HashFunctionType<TKey> hashFunction = null,
            int allocatedSize = 1)
        {
            if (allocatedSize <= 0)
            {
                throw new ArgumentException("Size must be >= 1");
            }

            this.hashFunction = hashFunction ?? GetDefaultHashCode<TKey>;

            this.size = 0;
            this.buckets = new ListStuff.List<TableElement>[allocatedSize];

            for (int i = 0; i < this.buckets.Length; ++i)
            {
                this.buckets[i] = new ListStuff.List<TableElement>();
            }
        }

        /// <summary>
        /// Gets hash table's fill factor
        /// </summary>
        public double FillFactor
        {
            get => (double)this.size / this.buckets.Length;
        }

        /// <summary>
        /// Allows to access hash table
        /// </summary>
        /// <param name="key">Identifier of the element</param>
        /// <returns>Value of requested element</returns>
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

        /// <summary>
        /// Adds (or modifies) element of the hash table
        /// </summary>
        /// <param name="key">Identifier of the element</param>
        /// <param name="value">Value of the new element</param>
        public void Add(TKey key, TValue value)
        {
            var element = this.FindElement(key);
            if (element != null)
            {
                element.Value = value;
                return;
            }

            var keyHash = this.GetBucketNumber(this.buckets, key);
            this.buckets[keyHash].Insert(new TableElement { Key = key, Value = value }, 0);

            ++this.size;

            this.OptimizeTableSize();
        }

        /// <summary>
        /// Check if element with given key is in table
        /// </summary>
        /// <param name="key">Identifier of the element</param>
        /// <returns>True if element with given key is in table</returns>
        public bool IsInTable(TKey key)
        {
            var element = this.FindElement(key);

            return element != null;
        }

        /// <summary>
        /// Erases element with given key (if exists)
        /// </summary>
        /// <param name="key">Identifier of the element</param>
        public void Erase(TKey key)
        {
            if (!this.IsInTable(key))
            {
                return;
            }

            var keyHash = this.GetBucketNumber(this.buckets, key);

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

        /// <summary>
        /// Returns default hash function
        /// </summary>
        /// <typeparam name="T">The type of the element whose hash is computed</typeparam>
        /// <param name="element">Element whose hash is computed</param>
        /// <returns>Integer hash</returns>
        private static int GetDefaultHashCode<T>(T element)
        {
            return element.GetHashCode();
        }

        /// <summary>
        /// Returns bucket number for given key (based on hash)
        /// </summary>
        /// <param name="buckets">Reference to bucket array</param>
        /// <param name="key">Identifier of the element whose number is computed</param>
        /// <returns>Number of bucket where element is placed</returns>
        private int GetBucketNumber(ListStuff.List<TableElement>[] buckets, TKey key) =>
            Math.Abs(this.hashFunction(key)) % buckets.Length;

        /// <summary>
        /// Returns reference to the element with given key
        /// </summary>
        /// <param name="key">Identifier of the element</param>
        /// <returns>Reference to the element or <see cref="null"/> if not found</returns>
        private TableElement FindElement(TKey key)
        {
            var keyHash = this.GetBucketNumber(this.buckets, key);

            foreach (TableElement element in this.buckets[keyHash])
            {
                if (element.Key.Equals(key))
                {
                    return element;
                }
            }

            return null;
        }

        /// <summary>
        /// Increases the number of buckets in the table if it is full
        /// </summary>
        private void OptimizeTableSize()
        {
            if (this.FillFactor > MaxFillFactor)
            {
                var newBucketCount = this.buckets.Length * ResizeFactor;

                var newBuckets = new ListStuff.List<TableElement>[newBucketCount];
                for (int i = 0; i < newBucketCount; ++i)
                {
                    newBuckets[i] = new ListStuff.List<TableElement>();
                }

                foreach (var bucket in this.buckets)
                {
                    foreach (TableElement element in bucket)
                    {
                        var newHash = this.GetBucketNumber(newBuckets, element.Key);
                        newBuckets[newHash].Insert(element, 0);
                    }
                }

                this.buckets = newBuckets;
            }
        }

        /// <summary>
        /// Single element in bucket
        /// </summary>
        private class TableElement
        {
            /// <summary>
            /// Gets or sets element identifier (key)
            /// </summary>
            public TKey Key { get; set; }

            /// <summary>
            /// Gets or sets element value
            /// </summary>
            public TValue Value { get; set; }
        }
    }
}