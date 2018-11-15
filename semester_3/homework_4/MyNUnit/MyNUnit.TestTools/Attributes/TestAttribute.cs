﻿namespace MyNUnit.TestTools
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class TestAttribute : Attribute
    {
        public Type Expected { get; }

        public string IgnoreReason { get; }
    }
}
