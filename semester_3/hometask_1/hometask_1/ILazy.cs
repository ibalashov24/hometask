namespace LazyStuff
{
    /// <summary>
    /// Implements lazy computation of the function
    /// </summary>
    /// <typeparam name="T">Type of result of the computation function</typeparam>
    public interface ILazy<T>
    {
        /// <summary>
        /// Get computation result
        /// </summary>
        /// <returns>Computation result</returns>
        T Get();
    }
}