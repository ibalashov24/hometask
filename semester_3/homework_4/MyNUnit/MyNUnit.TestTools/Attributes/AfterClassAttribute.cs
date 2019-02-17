namespace MyNUnit.TestTools
{
    using System;

    /// <summary>
    /// Marks methods which should be run one time after all tests
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AfterClassAttribute : Attribute
    {}
}
