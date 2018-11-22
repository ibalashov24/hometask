namespace MyNUnit.TestTools
{
    using System;

    /// <summary>
    /// Marks test methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TestAttribute : Attribute
    {
        /// <summary>
        /// If exception of given type was thrown then test succeed
        /// </summary>
        public Type Expected { get; set; }

        /// <summary>
        /// If is not empty when test will be ignored
        /// </summary>
        public string IgnoreReason { get; set; }
    }
}
