namespace MunCode.Core.Reflection.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using MunCode.Core.Guards;

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "Enumerable is enumerated only once.")]
    public static class AssemblyScanResultEnumerableExtensions
    {
        public static IEnumerable<TypeScanResult> ScanForTypes(this IEnumerable<AssemblyScanResult> enumerable)
        {
            Guard.NotNull(enumerable, nameof(enumerable));
            return enumerable.ScanForTypes(t => true);
        }

        public static IEnumerable<TypeScanResult> ScanForTypes(this IEnumerable<AssemblyScanResult> enumerable, Func<Type, bool> predicate)
        {
            Guard.NotNull(predicate, nameof(predicate));
            Guard.NotNull(enumerable, nameof(enumerable));
            foreach (var assemblyScanResult in enumerable)
            {
                var types = assemblyScanResult.Assembly.GetTypes().Where(predicate);

                foreach (var type in types)
                {
                    yield return new TypeScanResult(type);
                }
            }
        }
    }
}