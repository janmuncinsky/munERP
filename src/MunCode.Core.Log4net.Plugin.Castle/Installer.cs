namespace MunCode.Core.Log4net.Plugin.Castle
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using Microsoft.Extensions.Logging;

    using MunCode.Core.AppHosting;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new LoggerResolver(container.Kernel));
            container.Register(
                Component.For<IAppInitializer>().ImplementedBy<Log4netAppInitializer>().LifestyleSingleton(),
                Component.For(typeof(ILogger<>)).ImplementedBy(typeof(Logger<>)).LifestyleSingleton());
        }
    }
}