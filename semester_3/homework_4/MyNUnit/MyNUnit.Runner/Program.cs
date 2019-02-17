namespace MyNUnit.Runner
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Main program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method of the program
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var path = args[1];

            MainLoop(path);
        }

        /// <summary>
        /// Runs test from all assemblies on path
        /// </summary>
        /// <param name="path">Path to assemblies</param>
        public static void MainLoop(string path)
        {
            foreach (var assembly in LoadAllAssemblies(path))
            {
                var runner = new TestRunner(assembly);
                runner.RunAllTests();
            }
        }

        /// <summary>
        /// Loads all assemblies into the memory
        /// </summary>
        /// <param name="path">Path to assembly</param>
        /// <returns>Loaded assembly</returns>
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
