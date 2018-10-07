namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Events
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class EventConsumerAdapter<TEvent> : IMessageHandler<TEvent, EmptyResponse>
        where TEvent : IEvent
    {
        private readonly IEventConsumer<TEvent> eventConsumer;

        public EventConsumerAdapter(IEventConsumer<TEvent> eventConsumer)
        {
            Guard.NotNull(eventConsumer, nameof(eventConsumer));
            this.eventConsumer = eventConsumer;
        }

        public async Task<EmptyResponse> Handle(ReceiveContext<TEvent> context)
        {
            Guard.NotNull(context, nameof(context));
            await this.eventConsumer.Consume(context).ConfigureAwait(false);
            return EmptyResponse.Instance;
        }
    }
}