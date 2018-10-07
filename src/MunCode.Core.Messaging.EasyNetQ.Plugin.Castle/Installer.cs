namespace MunCode.Core.Messaging.EasyNetQ.Plugin.Castle
{
    using global::Castle.Facilities.TypedFactory;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using global::EasyNetQ;
    using global::EasyNetQ.ConnectionString;
    using global::EasyNetQ.DI.Windsor;
    using global::EasyNetQ.Logging;
    using global::EasyNetQ.Producer;

    using Microsoft.Extensions.Options;

    using MunCode.Core.AppHosting;
    using MunCode.Core.Logging;
    using MunCode.Core.Messaging.Endpoints;
    using MunCode.Core.Messaging.Endpoints.Input;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Serializaton;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RabbitHutch.RegisterBus(
                new WindsorAdapter(container),
                c =>
                    {
                        var connectionString = container.Resolve<IOptions<EasyNetQConfig>>().Value.ConnectionString;
                        return c.Resolve<IConnectionStringParser>().Parse(connectionString);
                    },
                s => { });

            container.Register(
                Component.For<IConfigureOptions<EasyNetQConfig>>().ImplementedBy<ConfigurationBinder<EasyNetQConfig>>().LifestyleSingleton(),
                Component.For<ITypeNameSerializer>().ImplementedBy<ShortNameTypeNameSerializer>().IsDefault().LifestyleSingleton(),
                Component.For<IRpc>().ImplementedBy<TransactionalRpc>().IsDefault().LifestyleSingleton(),
                Component.For<ILogProvider>().ImplementedBy<LoggerAdapterLogProvider>().LifestyleSingleton(),
                Component.For<IAppInitializer>().ImplementedBy<EasyNetQAppInitializer>().LifestyleSingleton(),
                Component.For<IEasyNetQMessageHandlerDispatcher>().ImplementedBy<EasyNetQMessageHandlerDispatcher>().LifestyleSingleton(),
                Component.For<IMessageConsumerInitializerFactory>().AsFactory().LifestyleSingleton(),
                Component.For(typeof(CommandConsumerInitializer<,>)).LifestyleSingleton(),
                Component.For(typeof(EventConsumerInitializer<,>)).LifestyleSingleton(),
                Component.For(typeof(RequestConsumerInitializer<,>)).LifestyleSingleton());
            container.RegisterInputMessageChannel<EasyNetQConsumerInitializerVisitor, EasyNetQInputConfig>();
            container.RegisterOutputMessageChannel<EasyNetQOutputMessageChannel, EasyNetQOutputConfig>();
        }
    }
}