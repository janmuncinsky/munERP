namespace MunCode.Core.Reflection.Scanning
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using MunCode.Core.Guards;

    public static class AssemblyScanner
    {
        public static IEnumerable<AssemblyScanResult> ScanForAssemblies(string searchPattern)
        {
            return Enumerable.Empty<AssemblyScanResult>().ScanForAssemblies(searchPattern);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "Enumerable is enumerated only once.")]
        public static IEnumerable<AssemblyScanResult> ScanForAssemblies(this IEnumerable<AssemblyScanResult> enumerable, string searchPattern)
        {
            Guard.NotNull(enumerable, nameof(enumerable));
            Guard.NotNull(searchPattern, nameof(searchPattern));

            foreach (var result in enumerable)
            {
                yield return result;
            }
    
            var rootAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            string path = Path.GetDirectoryName(rootAssembly.Location);

            foreach (var assemblyName in Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories))
            {
                var assembly = Assembly.LoadFrom(assemblyName);
                yield return new AssemblyScanResult(assembly);
            }
        }
    }
}