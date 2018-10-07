namespace MunCode.Core.Ioc
{
    using System.Composition.Hosting;
    using System.Linq;

    using MunCode.Core.Guards;
    using MunCode.Core.Reflection.Scanning;

    public static class ComponentComposer
    {
        public static T Compose<T>(string searchPattern)
        {
            Guard.NotNull(searchPattern, nameof(searchPattern));
            var assemblies = AssemblyScanner
                .ScanForAssemblies(searchPattern)
                .Select(a => a.Assembly);

            var configuration = new ContainerConfiguration().WithAssemblies(assemblies);

            using (var container = configuration.CreateContainer())
            {
                return container.GetExport<T>();
            }
        }
    }
}