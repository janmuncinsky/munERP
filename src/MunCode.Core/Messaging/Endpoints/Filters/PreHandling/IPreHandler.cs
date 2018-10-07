namespace MunCode.Core.Messaging.Endpoints.Filters.PreHandling
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IPreHandler<TMessage>
    {
        Task Handle(MessageContext<TMessage> context);
    }
}