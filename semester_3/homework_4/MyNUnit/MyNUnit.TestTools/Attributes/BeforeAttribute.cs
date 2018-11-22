namespace MyNUnit.TestTools
{
    using System;

    /// <summary>
    /// Marks methods which should be run before each test method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class BeforeAttribute : Attribute
    {}
}
