namespace MunCode.Core.Messaging.Messages
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Output;

    public class ReceiveContext<TMessage> : MessageContext<TMessage>, IEventBus
    {
        private readonly IMessageContextFactory factory;
        private readonly IOutputMessageChannel channel;

        public ReceiveContext(TMessage message, MessageMetadata messageMetadata, IMessageContextFactory factory, IOutputMessageChannel channel)
            : base(message, messageMetadata)
        {
            Guard.NotNull(channel, nameof(channel));
            Guard.NotNull(factory, nameof(factory));
            this.factory = factory;
            this.channel = channel;
        }

        public Task Publish<TEvent>(TEvent @event)
            where TEvent : class, IEvent
        {
            Guard.NotNull(@event, nameof(@event));
            var context = this.factory.Create<SendContext<TEvent>, TEvent>(@event, this.MessageMetadata);
            return this.channel.Publish(context);
        }

        public Task Respond<TResponse>(TResponse response)
            where TResponse : class
        {
            Guard.NotNull(response, nameof(response));
            var context = this.factory.Create<SendContext<TResponse>, TResponse>(response, this.MessageMetadata);
            return this.channel.Respond(context);
        }

        public SendContext<TMessage> CreateSendContext()
        {
            return this.factory.Create<SendContext<TMessage>, TMessage>(this.Message, this.MessageMetadata);
        }
    }
}