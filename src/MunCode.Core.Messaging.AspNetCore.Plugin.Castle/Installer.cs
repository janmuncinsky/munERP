namespace MunCode.Core.Messaging.AspNetCore.Plugin.Castle
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using Microsoft.AspNetCore.Mvc.Infrastructure;

    using MunCode.Core.Messaging.Endpoints.Input;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;
    using MunCode.Core.Messaging.Endpoints.Output;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<InputMessageChannelApplicationPart>().LifestyleSingleton(),
                Component.For<IActionDescriptorChangeProvider>().ImplementedBy<InputMessageChannelProvider>().LifestyleSingleton());
            container.RegisterInputMessageChannel<AspNetCoreApplicationPartVisitor, AspNetCoreInputConfig>();
        }
    }
}