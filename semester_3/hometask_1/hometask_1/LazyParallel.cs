namespace LazyStuff
{
    using System;
    using System.Threading;

    /// <summary>
    /// Performs parallel lazy computation of the supplier function
    /// </summary>
    /// <typeparam name="T">Supplier result type</typeparam>
    public class LazyParallel<T> : ILazy<T>
    {
        /// <summary>
        /// Computation function
        /// </summary>
        private volatile Func<T> supplierFunction;

        /// <summary>
        /// Object to use in lock(){}
        /// </summary>
        private Object lockObject = new Object();

        /// <summary>
        /// If false then <see cref="supplierFunction"/> 
        /// will be called in <see cref="Get"/>
        /// </summary>
        private volatile bool isResultCalculated = false;

        /// <summary>
        /// Calculated result of the supplier
        /// </summary>
        private T computationResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyParallel{T}"/> class
        /// </summary>
        /// <param name="supplier">Computation function</param>
        public LazyParallel(Func<T> supplier)
        {
            this.supplierFunction = supplier;
        }

        public T Get()
        {
            if (!this.isResultCalculated)
            {
                lock (this.lockObject)
                {
                    if (!this.isResultCalculated)
                    {
                        this.computationResult = this.supplierFunction();

                        this.isResultCalculated = true;
                        this.supplierFunction = null;
                    }
                }
            }

            return this.computationResult;
        }
    }
}