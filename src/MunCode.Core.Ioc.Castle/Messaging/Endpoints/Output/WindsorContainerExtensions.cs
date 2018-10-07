namespace MunCode.Core.Messaging.Endpoints.Output
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using Microsoft.Extensions.Options;

    using MunCode.Core.AppHosting;
    using MunCode.Core.Messaging.Endpoints.Input;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;

    public static class WindsorContainerExtensions
    {
        public static void RegisterInputMessageChannel<TConsumerDefinitionVisitor, TConfig>(this IWindsorContainer container)
            where TConfig : class, IMessageChannelConfig, new() 
            where TConsumerDefinitionVisitor : class, IConsumerDefinitionVisitor
        {
            container.Register(
                Component.For<TConsumerDefinitionVisitor>().LifestyleSingleton(),
                Component.For<IConsumerDefinitionVisitor>().ImplementedBy<InputMessageChannelDefinition<MessageChannelConfigurator<TConfig>, TConsumerDefinitionVisitor>>().LifestyleSingleton(),
                Component.For<IConfigureOptions<TConfig>>().ImplementedBy<ConfigurationBinder<TConfig>>().LifestyleSingleton());
        }

        public static void RegisterOutputMessageChannel<TChannel, TConfig>(this IWindsorContainer container)
            where TConfig : class, IMessageChannelConfig, new() 
            where TChannel : class, IOutputMessageChannel
        {
            container.Register(
                Component.For<IMessageChannelConfigurator>().ImplementedBy<MessageChannelConfigurator<TConfig>>().LifestyleSingleton(),
                Component.For<IConfigureOptions<TConfig>>().ImplementedBy<ConfigurationBinder<TConfig>>().LifestyleSingleton(),
                Component.For<TChannel>().LifestyleSingleton(),
                Component.For<IOutputMessageChannelDefinition>().ImplementedBy<OutputMessageChannelDefinition<MessageChannelConfigurator<TConfig>, TChannel>>().LifestyleSingleton());
        }
    }
}