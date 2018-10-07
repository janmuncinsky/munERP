namespace MunCode.Core.Messaging.Endpoints.Filters.PostHandling
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IPostHandler<TMessage, in TResponse>
    {
        Task Handle(MessageContext<TMessage> context, TResponse response);
    }
}