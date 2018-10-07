namespace MunCode.mERP.Sales.Model.Messages.Responses
{
    using MunCode.mERP.Sales.Model.Read;

    public class OrderStatusResponse
    {
        public OrderStatusResponse(OrderStatus orderStatus)
        {
            this.OrderStatus = orderStatus;
        }

        public OrderStatus OrderStatus { get; }
    }
}