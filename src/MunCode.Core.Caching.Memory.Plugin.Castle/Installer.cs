namespace MunCode.Core.Caching.Memory.Plugin.Castle
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMemoryCache>().ImplementedBy<MemoryCache>().LifestyleSingleton(),
                Component.For<IOptions<MemoryCacheOptions>>().Instance(new MemoryCacheOptions()).LifestyleSingleton());
        }
    }
}
