using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace MyNUnit.Runner
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("MyNUnit Test Runner");
            Console.Write("Enter path to assemblies: ");

            var path = Console.ReadLine();

            foreach (var assembly in LoadAllAssemblies(path))
            {
                var runner = new TestRunner(assembly);
                runner.RunAllTestsAsync().GetAwaiter().GetResult();
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
                catch (FileLoadException)
                {
                    continue;
                }

                yield return currentAssembly;
            }
        }
    }
}
