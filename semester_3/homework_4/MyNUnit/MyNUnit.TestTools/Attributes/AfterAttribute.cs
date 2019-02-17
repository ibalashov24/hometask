namespace MyNUnit.TestTools
{
    using System;

    /// <summary>
    /// Marks methods which should be run after each test
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AfterAttribute : Attribute
    {}
}
