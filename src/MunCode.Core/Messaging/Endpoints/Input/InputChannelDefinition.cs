namespace MunCode.Core.Messaging.Endpoints.Input
{
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;

    public class InputMessageChannelDefinition<TMessageChannelConfigurator, TConsumerDefinitionVisitor> : IConsumerDefinitionVisitor
        where TMessageChannelConfigurator : IMessageChannelConfigurator
        where TConsumerDefinitionVisitor : IConsumerDefinitionVisitor
    {
        private readonly TMessageChannelConfigurator configurator;
        private readonly TConsumerDefinitionVisitor visitor;

        public InputMessageChannelDefinition(TMessageChannelConfigurator configurator, TConsumerDefinitionVisitor visitor)
        {
            Guard.NotNull(visitor, nameof(visitor));
            Guard.NotNull(configurator, nameof(configurator));
            this.configurator = configurator;
            this.visitor = visitor;
        }

        public void Visit<TCommand, TConsumer>(CommandConsumerDefinition<TCommand, TConsumer> commandConsumerDefinition)
            where TCommand : class, ICommand where TConsumer : ICommandConsumer<TCommand>
        {
            Guard.NotNull(commandConsumerDefinition, nameof(commandConsumerDefinition));
            if (this.configurator.CanRoute(commandConsumerDefinition.MessageType))
            {
                this.visitor.Visit(commandConsumerDefinition);
            }
        }

        public void Visit<TEvent, TConsumer>(EventConsumerDefinition<TEvent, TConsumer> eventConsumerDefinition)
            where TEvent : class, IEvent where TConsumer : IEventConsumer<TEvent>
        {
            Guard.NotNull(eventConsumerDefinition, nameof(eventConsumerDefinition));
            if (this.configurator.CanRoute(eventConsumerDefinition.MessageType))
            {
                this.visitor.Visit(eventConsumerDefinition);
            }
        }

        public void Visit<TRequest, TResponse, TConsumer>(RequestConsumerDefinition<TRequest, TResponse, TConsumer> requestConsumerDefinition)
            where TRequest : class, IRequest<TResponse> where TResponse : class where TConsumer : IRequestConsumer<TRequest, TResponse>
        {
            Guard.NotNull(requestConsumerDefinition, nameof(requestConsumerDefinition));
            if (this.configurator.CanRoute(requestConsumerDefinition.MessageType))
            {
                this.visitor.Visit(requestConsumerDefinition);
            }
        }
    }
}