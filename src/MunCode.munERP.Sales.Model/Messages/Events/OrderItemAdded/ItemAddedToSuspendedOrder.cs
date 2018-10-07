namespace MunCode.munERP.Sales.Model.Messages.Events.OrderItemAdded
{
    public class ItemAddedToSuspendedOrder : OrderItemAdded
    {
        public ItemAddedToSuspendedOrder(OrderItemAddedData data)
            : base(data)
        {
        }
    }
}