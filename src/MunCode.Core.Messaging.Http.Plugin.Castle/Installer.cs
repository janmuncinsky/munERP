namespace MunCode.Core.Messaging.Http.Plugin.Castle
{
    using System.Net.Http;

    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using Microsoft.Extensions.Options;

    using MunCode.Core.AppHosting;
    using MunCode.Core.Messaging.Endpoints.Output;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IConfigureOptions<HttpMessageBusConfig>>().ImplementedBy<ConfigurationBinder<HttpMessageBusConfig>>().LifestyleSingleton(),
                Component.For<HttpClient>().LifestyleSingleton());
            container.RegisterOutputMessageChannel<HttpOutputMessageChannel, HttpMessageBusOutputConfig>();
        }
    }
}
