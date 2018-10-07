namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Events
{
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class EventConsumerDefinition<TEvent, TConsumer> : ConsumerDefinition<TEvent, EmptyResponse, TConsumer>
        where TEvent : class, IEvent
        where TConsumer : IEventConsumer<TEvent>
    {
        public override void Accept(IConsumerDefinitionVisitor visitor)
        {
            Guard.NotNull(visitor, nameof(visitor));
            visitor.Visit(this);
        }
    }
}