namespace MunCode.mERP.Accounting.Model.Messages.Events.OrderItemAdded
{
    public class ItemAddedToAcceptedOrder : OrderItemAdded
    {
        public ItemAddedToAcceptedOrder(OrderItemAddedData data)
            : base(data)
        {
        }
    }
}