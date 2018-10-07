namespace MunCode.Core.Messaging.Messages
{
    public interface ITransaction<TResult> : ICommand, IRequest<TResult>
    {
    }
}