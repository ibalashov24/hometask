namespace MyNUnit.TestTools
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TestClassAttribute : Attribute
    {}
}
