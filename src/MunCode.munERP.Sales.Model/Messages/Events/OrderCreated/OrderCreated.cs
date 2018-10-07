namespace MunCode.munERP.Sales.Model.Messages.Events.OrderCreated
{
    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public abstract class OrderCreated : IEvent, IAggregateCreatedEvent
    {
        protected OrderCreated(OrderCreatedData data)
        {
            Guard.NotNull(data, nameof(data));
            this.Data = data;
        }

        public OrderCreatedData Data { get; }
    }
}