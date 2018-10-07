namespace MunCode.Core.Messaging.Messages
{
    using MunCode.Core.Guards;

    public class SendContext<TMessage> : MessageContext<TMessage>
    {
        private readonly IMessageContextFactory factory;

        public SendContext(TMessage message, MessageMetadata messageMetadata, IMessageContextFactory factory)
            : base(message, messageMetadata)
        {
            Guard.NotNull(factory, nameof(factory));
            this.factory = factory;
        }

        public ReceiveContext<TMessage> CreateReceiveContext()
        {
            return this.factory.Create<ReceiveContext<TMessage>, TMessage>(this.Message, this.MessageMetadata);
        }
    }
}