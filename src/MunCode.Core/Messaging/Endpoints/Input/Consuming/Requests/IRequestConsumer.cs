namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IRequestConsumer<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        Task<TResponse> Consume(ReceiveContext<TRequest> messageContext);
    }
}