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
        private readonly Func<T> supplierFunction;

        /// <summary>
        /// Mutex for syncronising calls of supplier
        /// </summary>
        private readonly Mutex supplierFunctionMutex = new Mutex();

        /// <summary>
        /// If false then <see cref="supplierFunction"/> 
        /// will be called in <see cref="Get"/>
        /// </summary>
        private bool isResultCalculated = false;

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
                this.supplierFunctionMutex.WaitOne();

                if (!this.isResultCalculated)
                {
                    this.computationResult = this.supplierFunction();
                    this.isResultCalculated = true;
                }

                this.supplierFunctionMutex.ReleaseMutex();
            }

            return this.computationResult;
        }
    }
}