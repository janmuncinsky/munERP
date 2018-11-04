namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IInputMessageChannel
    {
        Task<TResponse> Dispatch<TMessage, TResponse>(ReceiveContext<TMessage> context);
    }
}