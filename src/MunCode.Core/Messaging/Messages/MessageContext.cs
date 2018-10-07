namespace MunCode.Core.Messaging.Messages
{
    using MunCode.Core.Guards;

    public class MessageContext<TMessage>
    {
        protected MessageContext(TMessage message, MessageMetadata messageMetadata)
        {
            Guard.NotNull(messageMetadata, nameof(messageMetadata));
            Guard.NotNull(message, nameof(message));
            this.Message = message;
            this.MessageMetadata = messageMetadata;
        }

        public TMessage Message { get; }

        public MessageMetadata MessageMetadata { get; }
    }
}
