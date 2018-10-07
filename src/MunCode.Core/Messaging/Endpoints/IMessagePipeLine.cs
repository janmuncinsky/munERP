namespace MunCode.Core.Messaging.Endpoints
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Messages;

    public interface IMessagePipeLine
    {
        Task<TResponse> Dispatch<TContext, TMessage, TResponse>(TContext context, MessageHandlerDelegate<TContext, TMessage, TResponse> endDelegate)
            where TContext : MessageContext<TMessage>;
    }
}