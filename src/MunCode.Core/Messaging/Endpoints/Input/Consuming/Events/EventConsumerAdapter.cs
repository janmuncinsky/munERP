namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Events
{
    using System.Linq;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class EventConsumerAdapter<TEvent> : IMessageHandler<TEvent, EmptyResponse>
        where TEvent : IEvent
    {
        private readonly ITopicDispatcher topicDispatcher;
        private readonly IEventConsumer<TEvent>[] eventConsumers;

        public EventConsumerAdapter(IEventConsumer<TEvent>[] eventConsumers, ITopicDispatcher topicDispatcher)
        {
            Guard.NotNull(topicDispatcher, nameof(topicDispatcher));
            Guard.NotNull(eventConsumers, nameof(eventConsumers));
            this.eventConsumers = eventConsumers;
            this.topicDispatcher = topicDispatcher;
        }

        public async Task<EmptyResponse> Handle(ReceiveContext<TEvent> context)
        {
            Guard.NotNull(context, nameof(context));
            await Task.WhenAll(
                this.eventConsumers
                    .Where(e => this.topicDispatcher.CanConsumeTopic(e.GetType(), context.MessageMetadata.Topic))
                    .Select(e => e.Consume(context))).ConfigureAwait(false);
            return EmptyResponse.Instance;
        }
    }
}