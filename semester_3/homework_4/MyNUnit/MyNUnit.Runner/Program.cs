namespace MyNUnit.Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var path = args[1];

            MainLoop(path);
        }

        public static void MainLoop(string path)
        {
            foreach (var assembly in LoadAllAssemblies(path))
            {
                var runner = new TestRunner(assembly);
                runner.RunAllTestsAsync();
            }
        }

        public static IEnumerable<Assembly> LoadAllAssemblies(string path)
        {
            if (File.Exists(path))
            {
                yield return Assembly.LoadFrom(path);
                yield break;
            }

            var directoryFiles = Directory.GetFiles(path);
            foreach (var file in directoryFiles)
            {
                Assembly currentAssembly;
                try
                {
                    currentAssembly = Assembly.LoadFrom(file);
                }
                catch (Exception)
                {
                    continue;
                }

                yield return currentAssembly;
            }
        }
    }
}
