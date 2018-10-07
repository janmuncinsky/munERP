namespace MunCode.munERP.Sales.Model.Messages.Responses
{
    using MunCode.munERP.Sales.Model.Read;

    public class OrderStatusResponse
    {
        public OrderStatusResponse(OrderStatus orderStatus)
        {
            this.OrderStatus = orderStatus;
        }

        public OrderStatus OrderStatus { get; }
    }
}