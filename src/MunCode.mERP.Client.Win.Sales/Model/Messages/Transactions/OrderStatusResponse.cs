namespace MunCode.mERP.Client.Win.Sales.Model.Messages.Transactions
{
    public class OrderStatusResponse
    {
        public OrderStatusResponse(OrderStatus orderStatus)
        {
            this.OrderStatus = orderStatus;
        }

        public OrderStatus OrderStatus { get; }
    }
}