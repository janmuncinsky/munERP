namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IEventBus
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}