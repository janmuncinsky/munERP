namespace MunCode.munERP.Sales.Model.Messages.Events.OrderCreated
{
    public class SuspendedOrderCreated : OrderCreated
    {
        public SuspendedOrderCreated(OrderCreatedData data)
            : base(data)
        {
        }
    }
}