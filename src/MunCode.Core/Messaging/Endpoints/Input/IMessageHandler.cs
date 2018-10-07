namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IMessageHandler<TMessage, TResponse>
    {
        Task<TResponse> Handle(ReceiveContext<TMessage> message);
    }
}