namespace MunCode.mERP.Sales.Model.Messages.Events.OrderCreated
{
    public class AcceptedOrderCreated : OrderCreated
    {
        public AcceptedOrderCreated(OrderCreatedData data)
            : base(data)
        {
        }
    }
}