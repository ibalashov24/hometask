namespace LazyStuff
{
    using System;

    /// <summary>
    /// Performs lazy computation of the supplier function
    /// </summary>
    /// <typeparam name="T">Supplier result type</typeparam>
    public class Lazy<T> : ILazy<T>
    {
        /// <summary>
        /// Computation function
        /// </summary>
        private Func<T> supplierFunction;

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
        /// Initializes a new instance of the <see cref="Lazy{T}"/> class
        /// </summary>
        /// <param name="supplier">Computation function</param>
        public Lazy(Func<T> supplier)
        {
            this.supplierFunction = supplier;
        }

        /// <summary>
        /// Calculates the result of the supplier computation
        /// </summary>
        /// <returns>
        /// Returns computation result
        /// (the same object after every call)
        /// </returns>
        public T Get()
        {
            if (!this.isResultCalculated)
            {
                this.computationResult = this.supplierFunction();

                this.isResultCalculated = true;
                this.supplierFunction = null;
            }

            return this.computationResult;
        }
    }
}
