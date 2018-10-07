namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IRequestBus
    {
        Task<TResponse> Request<TRequest, TResponse>(TRequest request)
            where TRequest : class, IRequest<TResponse>
            where TResponse : class;
    }
}