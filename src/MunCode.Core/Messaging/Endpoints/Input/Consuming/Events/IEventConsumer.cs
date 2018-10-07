namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Events
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IEventConsumer<TEvent>
        where TEvent : IEvent
    {
        Task Consume(ReceiveContext<TEvent> messageContext);
    }
}
