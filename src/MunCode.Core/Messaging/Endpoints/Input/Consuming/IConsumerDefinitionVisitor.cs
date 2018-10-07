namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;

    public interface IConsumerDefinitionVisitor
    {
        void Visit<TCommand, TConsumer>(CommandConsumerDefinition<TCommand, TConsumer> commandConsumerDefinition)
            where TCommand : class, ICommand
            where TConsumer : ICommandConsumer<TCommand>;


        void Visit<TEvent, TConsumer>(EventConsumerDefinition<TEvent, TConsumer> eventConsumerDefinition)
            where TEvent : class, IEvent
            where TConsumer : IEventConsumer<TEvent>;

        void Visit<TRequest, TResponse, TConsumer>(RequestConsumerDefinition<TRequest, TResponse, TConsumer> requestConsumerDefinition)
            where TRequest : class, IRequest<TResponse>
            where TResponse : class
            where TConsumer : IRequestConsumer<TRequest, TResponse>;
    }
}