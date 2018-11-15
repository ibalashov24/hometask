namespace MyNUnit.TestTools
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AfterClassAttribute : Attribute
    {}
}
