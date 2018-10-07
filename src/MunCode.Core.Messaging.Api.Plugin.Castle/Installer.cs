namespace MunCode.Core.Messaging.Api.Plugin.Castle
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using MunCode.Core.AppHosting;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IAppInitializer>().ImplementedBy<ApiAppInitializer>().LifestyleSingleton());
        }
    }
}