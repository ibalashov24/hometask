namespace MyNUnit.TestTools
{
    using System;


    /// <summary>
    /// Marks methods which should be run one time before all test methods
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class BeforeClassAttribute : Attribute
    {}
}
