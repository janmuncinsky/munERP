namespace MunCode.mERP.Sales.Model.Messages.Events.OrderItemAdded
{
    public class ItemAddedToUnsuspendedOrder : OrderItemAdded
    {
        public ItemAddedToUnsuspendedOrder(OrderItemAddedData data)
            : base(data)
        {
        }
    }
}