namespace LazyStuff
{
    using System;

    /// <summary>
    /// Creates new instance of ILazy<T> of certain type
    /// </summary>
    /// <typeparam name="T">
    /// Lazy computation function return value type
    /// </typeparam>
    public static class LazyFactory<T>
    {
        /// <summary>
        /// Creates new instance of Lazy<typeparamref name="T"/>
        /// </summary>
        /// <param name="supplier">Computation function</param>
        /// <returns>New Lazy<typeparamref name="T"/> instance</returns>
        public static ILazy<T> CreateLazy(Func<T> supplier)
            => new Lazy<T>(supplier);

        /// <summary>
        /// Creates new instance of LazyParallel<typeparamref name="T"/>
        /// </summary>
        /// <param name="supplier">Computation function</param>
        /// <returns>New LazyParallel<typeparamref name="T"/> instance</returns>
        public static ILazy<T> CreateParallelLazy(Func<T> supplier)
            => new LazyParallel<T>(supplier);
    }
}