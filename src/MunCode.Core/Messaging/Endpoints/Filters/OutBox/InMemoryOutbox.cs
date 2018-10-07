namespace MunCode.Core.Messaging.Endpoints.Filters.OutBox
{
    using System.Collections.Generic;
    using System.Linq;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class InMemoryOutbox : IOutbox
    {
        private readonly Queue<IEvent> events = new Queue<IEvent>();

        public IEnumerable<IEvent> Dequeue()
        {
            return this.events.AsEnumerable();
        }

        public void Enqueue<TEvent>(TEvent @event)
            where TEvent : class, IEvent
        {
            Guard.NotNull(@event, nameof(@event));
            this.events.Enqueue(@event);
        }
    }
}