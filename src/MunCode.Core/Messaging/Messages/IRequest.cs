namespace MunCode.Core.Messaging.Messages
{
    public interface IRequest
    {
    }

    public interface IRequest<TResponse> : IRequest
    {
    }
}