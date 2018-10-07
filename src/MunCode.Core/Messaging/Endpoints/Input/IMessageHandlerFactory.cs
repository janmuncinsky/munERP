namespace MunCode.Core.Messaging.Endpoints.Input
{
    public interface IMessageHandlerFactory
    {
        IMessageHandler<TMessage, TResponse> Create<TMessage, TResponse>();
    }
}
