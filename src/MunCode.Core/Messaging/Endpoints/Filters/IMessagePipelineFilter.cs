namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public delegate Task<TResponse> MessageHandlerDelegate<in TMessageContext, TMessage, TResponse>(TMessageContext context) where TMessageContext : MessageContext<TMessage>;

    public interface IMessagePipelineFilter<TMessageContext, TMessage, TResponse>
        where TMessageContext : MessageContext<TMessage>
    {
        Task<TResponse> Handle(TMessageContext context, MessageHandlerDelegate<TMessageContext, TMessage, TResponse> nextHandler);
    }
}