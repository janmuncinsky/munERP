namespace MunCode.Core.Messaging.Plugin.Castle
{
    using global::Castle.Facilities.TypedFactory;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using MunCode.Core.AppHosting;
    using MunCode.Core.Ioc;
    using MunCode.Core.Messaging.Endpoints;
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Endpoints.Filters.ExceptionHandling;
    using MunCode.Core.Messaging.Endpoints.Filters.OutBox;
    using MunCode.Core.Messaging.Endpoints.Filters.PostHandling;
    using MunCode.Core.Messaging.Endpoints.Filters.PreHandling;
    using MunCode.Core.Messaging.Endpoints.Input;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(SendContext<>)).LifestyleTransient(),
                Component.For(typeof(ReceiveContext<>)).LifestyleTransient(),
                Component.For(typeof(IMessagePipelineFilter<,,>)).ImplementedBy(typeof(ExceptionHandlerFilter<,,>)).LifestyleSingleton(),
                Component.For(typeof(IMessagePipelineFilter<,,>)).ImplementedBy(typeof(PreHandlerFilter<,,>)).LifestyleSingleton(),
                Component.For(typeof(IMessagePipelineFilter<,,>)).ImplementedBy(typeof(PostHandlerFilter<,,>)).LifestyleSingleton(),
                Component.For(typeof(IMessagePipelineFilter<,,>)).ImplementedBy(typeof(CacheFilter<,,>)).LifestyleSingleton(),
                Component.For(typeof(IMessagePipelineFilter<,,>)).ImplementedBy(typeof(OutBoxPublisherFilter<,>), new ReceiveContextMatchingStrategy(), new ReceiveContextServiceStrategy()).LifestyleScoped(),
                Component.For<IOutbox>().ImplementedBy<InMemoryOutbox>().LifestyleScoped(),
                Component.For(typeof(IExceptionHandler<>)).ImplementedBy(typeof(LoggingExceptionHandler<>)).LifestyleSingleton(),
                Component.For(typeof(IPostHandler<,>)).ImplementedBy(typeof(LoggingPostHandler<,>)).LifestyleSingleton(),
                Component.For(typeof(IPreHandler<>)).ImplementedBy(typeof(LoggingPreHandler<>)).LifestyleSingleton(),
                Component.For<IHostInitializer>().ImplementedBy<ConsumerDefinitionVisitorInitializer>().LifestyleSingleton(),
                Component.For<IMessagePipeLine>().ImplementedBy<MessagePipeLine>().LifestyleSingleton(),
                Component.For<IMessageHandlerFiltersFactory>().AsFactory().LifestyleSingleton(),
                Component.For<IMessageContextFactory>().AsFactory().LifestyleSingleton(),
                Component.For<IInputMessageChannel>().ImplementedBy<InputMessageChannel>().LifestyleSingleton(),
                Component.For<IMessageHandlerFactory>().AsFactory().LifestyleSingleton(),
                Component.For<IMessageBus, IRequestBus, ICommandBus, IEventBus>().ImplementedBy<MediatorBus>().LifestyleSingleton(),
                Component.For<IOutputMessageChannel>().ImplementedBy<MessageRouter>().LifestyleSingleton(),
                Component.For<ITopicDispatcher>().ImplementedBy<TopicDispatcher>().LifestyleSingleton(),
                Component.For(typeof(MessageChannelConfigurator<>)).LifestyleSingleton());
            container.RegisterOutputMessageChannel<InMemoryOutputMessageChannel, InMemoryOutputConfig>();
        }
    }
}
