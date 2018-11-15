﻿namespace MyNUnit.Runner
{
    using System.Collections.Generic;
    using System.Reflection;

    public class TestMethodsContainer
    {
        public List<MethodInfo> BeforeClassMethods { get; set; }
                = new List<MethodInfo>();

        public List<MethodInfo> AfterClassMethods { get; set; }
                = new List<MethodInfo>();

        public List<MethodInfo> BeforeMethods { get; set; }
                = new List<MethodInfo>();

        public List<MethodInfo> AfterMethods { get; set; }
                = new List<MethodInfo>();

        public List<MethodInfo> TestMethods { get; set; }
                = new List<MethodInfo>();
    }
}