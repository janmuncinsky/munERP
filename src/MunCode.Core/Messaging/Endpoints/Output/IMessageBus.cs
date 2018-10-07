namespace MunCode.Core.Messaging.Endpoints.Output
{
    public interface IMessageBus : ICommandBus, IEventBus, IRequestBus
    {
    }
}