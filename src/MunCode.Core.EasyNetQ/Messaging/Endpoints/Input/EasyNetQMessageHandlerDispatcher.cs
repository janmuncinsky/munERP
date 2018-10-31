namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System.Threading.Tasks;

    using global::EasyNetQ;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class EasyNetQMessageHandlerDispatcher : IEasyNetQMessageHandlerDispatcher
    {
        private readonly IInputMessageChannel dispatcher;
        private readonly IMessageContextFactory factory;

        public EasyNetQMessageHandlerDispatcher(IInputMessageChannel dispatcher, IMessageContextFactory factory)
        {
            Guard.NotNull(factory, nameof(factory));
            Guard.NotNull(dispatcher, nameof(dispatcher));
            this.dispatcher = dispatcher;
            this.factory = factory;
        }

        public Task Dispatch<TMessage>(IMessage<TMessage> message, string topic)
        {
            Guard.NotNull(message, nameof(message));
            var metaData = new MessageMetadata(message.Properties.ReplyTo, message.Properties.CorrelationId);
            var context = this.factory.Create<ReceiveContext<TMessage>, TMessage>(message.Body, metaData);
            return this.dispatcher.Dispatch<TMessage, EmptyResponse>(context, topic);
        }
    }
}