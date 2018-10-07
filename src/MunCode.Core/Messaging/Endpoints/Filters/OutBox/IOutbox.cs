namespace MunCode.Core.Messaging.Endpoints.Filters.OutBox
{
    using System.Collections.Generic;

    using MunCode.Core.Messaging.Messages;

    public interface IOutbox
    {
        IEnumerable<IEvent> Dequeue();

        void Enqueue<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}