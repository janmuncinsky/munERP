namespace MunCode.munERP.Accounting.Model.Messages.Events.OrderCreated
{
    using MunCode.Core.Messaging.Messages;

    public class AcceptedOrderCreated : IEvent
    {
        public AcceptedOrderCreated(OrderCreatedData data)
        {
            this.Data = data;
        }

        public OrderCreatedData Data { get; }
    }
}