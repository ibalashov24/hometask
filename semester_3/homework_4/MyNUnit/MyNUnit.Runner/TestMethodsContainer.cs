namespace MyNUnit.Runner
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Contains class test methods of different types
    /// (with appropriate attribute)
    /// </summary>
    public class TestMethodsContainer
    {
        /// <summary>
        /// Methods with [BeforeClass] attribute
        /// </summary>
        public List<MethodInfo> BeforeClassMethods { get; set; }
                = new List<MethodInfo>();

        /// <summary>
        /// Methods with [AfterClass] attribute
        /// </summary>
        public List<MethodInfo> AfterClassMethods { get; set; }
                = new List<MethodInfo>();

        /// <summary>
        /// Methods with [Before] attribute
        /// </summary>
        public List<MethodInfo> BeforeMethods { get; set; }
                = new List<MethodInfo>();

        /// <summary>
        /// Methods with [After] attribute
        /// </summary>
        public List<MethodInfo> AfterMethods { get; set; }
                = new List<MethodInfo>();

        /// <summary>
        /// Methods with [Test] attribute
        /// </summary>
        public List<MethodInfo> TestMethods { get; set; }
                = new List<MethodInfo>();
    }
}
