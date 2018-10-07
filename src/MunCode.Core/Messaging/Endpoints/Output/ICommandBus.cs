namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}