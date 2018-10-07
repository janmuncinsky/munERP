namespace MunCode.Core.AppHosting.Plugin.Castle
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using Microsoft.Extensions.Options;

    using MunCode.Core.AppHosting;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IOptions<>)).ImplementedBy(typeof(OptionsManager<>)).LifestyleSingleton(),
                Component.For(typeof(IOptionsSnapshot<>)).ImplementedBy(typeof(OptionsManager<>)).LifestyleScoped().Named("OptionsManagerForSnapshot"),
                Component.For(typeof(IOptionsMonitor<>)).ImplementedBy(typeof(OptionsMonitor<>)).LifestyleSingleton(),
                Component.For(typeof(IOptionsFactory<>)).ImplementedBy(typeof(OptionsFactory<>)).LifestyleSingleton(),
                Component.For(typeof(IOptionsMonitorCache<>)).ImplementedBy(typeof(OptionsCache<>)).LifestyleSingleton(),
                Component.For<IConfigureOptions<AppConfig>>().ImplementedBy<AppConfigConfigurationBinder>().LifestyleSingleton(),
                Component.For<IAppInitializer>().ImplementedBy<PrimaryAppInitializer>().LifestyleSingleton().IsDefault());
        }
    }
}
