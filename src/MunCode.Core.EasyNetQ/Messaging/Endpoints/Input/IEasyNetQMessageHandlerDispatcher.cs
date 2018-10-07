namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System.Threading.Tasks;

    using global::EasyNetQ;

    public interface IEasyNetQMessageHandlerDispatcher
    {
        Task Dispatch<TMessage>(IMessage<TMessage> message);
    }
}