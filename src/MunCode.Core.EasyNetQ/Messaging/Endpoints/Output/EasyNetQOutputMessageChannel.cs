namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System.Threading.Tasks;

    using global::EasyNetQ;
    using global::EasyNetQ.Producer;
    using global::EasyNetQ.Topology;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class EasyNetQOutputMessageChannel : IOutputMessageChannel
    {
        private readonly IBus bus;
        private readonly ITypeNameSerializer serializer;
        private readonly IPublishExchangeDeclareStrategy publishExchangeDeclareStrategy;
        private readonly IMessageDeliveryModeStrategy messageDeliveryModeStrategy;

        public EasyNetQOutputMessageChannel(
            IBus bus,
            ITypeNameSerializer serializer,
            IPublishExchangeDeclareStrategy publishExchangeDeclareStrategy,
            IMessageDeliveryModeStrategy messageDeliveryModeStrategy)
        {
            Guard.NotNull(messageDeliveryModeStrategy, nameof(messageDeliveryModeStrategy));
            Guard.NotNull(publishExchangeDeclareStrategy, nameof(publishExchangeDeclareStrategy));
            Guard.NotNull(serializer, nameof(serializer));
            Guard.NotNull(bus, nameof(bus));
            this.bus = bus;
            this.serializer = serializer;
            this.publishExchangeDeclareStrategy = publishExchangeDeclareStrategy;
            this.messageDeliveryModeStrategy = messageDeliveryModeStrategy;
        }

        public Task Respond<TResponse>(SendContext<TResponse> context)
            where TResponse : class
        {
            var easyNetQMessage = new Message<TResponse>(context.Message)
            {
                Properties =
                    {
                        CorrelationId = context.MessageMetadata.CorrelationId.ToString(),
                        DeliveryMode = 1
                    }
            };

            var exchangeName = this.bus.Advanced.Conventions.RpcResponseExchangeNamingConvention(typeof(TResponse));
            var exchange = this.publishExchangeDeclareStrategy.DeclareExchange(exchangeName, "direct");
            return this.bus.Advanced.PublishAsync(
                exchange,
                context.MessageMetadata.ReplyTo,
                false,
                easyNetQMessage);
        }

        public Task Send<TCommand>(SendContext<TCommand> context)
            where TCommand : class, ICommand
        {
            var queueName = this.serializer.Serialize(typeof(TCommand));
            return this.bus.SendAsync(queueName, context.Message);
        }

        public async Task Publish<TEvent>(SendContext<TEvent> context)
            where TEvent : class, IEvent
        {
            var topic = this.bus.Advanced.Conventions.TopicNamingConvention(typeof(TEvent));
            var messageType = typeof(TEvent);

            var easyNetQMessage = this.CreateEasyNetQMessage(context);
            easyNetQMessage.Properties.DeliveryMode = this.messageDeliveryModeStrategy.GetDeliveryMode(messageType);
            IExchange exchange = await this.publishExchangeDeclareStrategy.DeclareExchangeAsync(messageType, "topic").ConfigureAwait(false);
            await this.bus.Advanced.PublishAsync(exchange, topic, false, easyNetQMessage).ConfigureAwait(false);
        }

        public Task<TResponse> Request<TRequest, TResponse>(SendContext<TRequest> context)
            where TRequest : class, IRequest<TResponse> 
            where TResponse : class
        {
            return this.bus.RequestAsync<TRequest, TResponse>(context.Message);
        }

        private Message<TMessage> CreateEasyNetQMessage<TMessage>(SendContext<TMessage> context)
            where TMessage : class
        {
            var easyNetQMessage = new Message<TMessage>(context.Message);
            if (context.MessageMetadata.CorrelationId != null)
            {
                easyNetQMessage.Properties.CorrelationId = context.MessageMetadata.CorrelationId.ToString();
            }

            if (context.MessageMetadata.ReplyTo != null)
            {
                easyNetQMessage.Properties.ReplyTo = context.MessageMetadata.ReplyTo;
            }

            return easyNetQMessage;
        }
    }
}