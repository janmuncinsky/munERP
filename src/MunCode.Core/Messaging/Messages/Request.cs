namespace MunCode.Core.Messaging.Messages
{
    using MunCode.Core.Design.Domain;

    public abstract class Request<TRequest, TResponse> : ValueObject<TRequest>, IRequest<TResponse>
        where TRequest : class, IRequest<TResponse>
    {
    }
}