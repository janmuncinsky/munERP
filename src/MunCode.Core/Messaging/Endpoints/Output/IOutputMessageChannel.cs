namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IOutputMessageChannel
    {
        Task Send<TCommand>(SendContext<TCommand> context) 
            where TCommand : class, ICommand;

        Task Publish<TEvent>(SendContext<TEvent> context) 
            where TEvent : class, IEvent;

        Task<TResponse> Request<TRequest, TResponse>(SendContext<TRequest> context)
            where TRequest : class, IRequest<TResponse>
            where TResponse : class;

        Task Respond<TResponse>(SendContext<TResponse> context)
            where TResponse : class;
    }
}