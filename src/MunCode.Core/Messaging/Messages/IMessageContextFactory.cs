namespace MunCode.Core.Messaging.Messages
{
    public interface IMessageContextFactory
    {
        TContext Create<TContext, TMessage>(TMessage message, MessageMetadata messageMetadata)
            where TContext : MessageContext<TMessage>;
    }
}