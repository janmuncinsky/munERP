namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface ICommandConsumer<TCommand>
        where TCommand : ICommand
    {
        Task Consume(ReceiveContext<TCommand> messageContext);
    }
}