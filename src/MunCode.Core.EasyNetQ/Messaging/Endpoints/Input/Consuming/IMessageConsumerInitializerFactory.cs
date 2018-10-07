namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;

    public interface IMessageConsumerInitializerFactory
    {
        CommandConsumerInitializer<TCommand, THandler> CreateCommandConsumerInitializer<TCommand, THandler>(CommandConsumerDefinition<TCommand, THandler> commandConsumerDefinition)
            where TCommand : class, ICommand
            where THandler : ICommandConsumer<TCommand>;

        EventConsumerInitializer<TEvent, THandler> CreateEventConsumerInitializer<TEvent, THandler>(EventConsumerDefinition<TEvent, THandler> eventConsumerDefinition)
            where TEvent : class, IEvent
            where THandler : IEventConsumer<TEvent>;

        RequestConsumerInitializer<TRequest, TResponse> CreateRequestConsumerInitializer<TRequest, TResponse>()
            where TRequest : class, IRequest<TResponse>
            where TResponse : class;
    }
}