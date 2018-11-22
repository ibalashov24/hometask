namespace MyNUnit.TestTools
{
    using System;

    /// <summary>
    /// Marks class with tests
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TestClassAttribute : Attribute
    {}
}
