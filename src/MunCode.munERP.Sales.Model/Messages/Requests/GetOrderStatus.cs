namespace MunCode.munERP.Sales.Model.Messages.Requests
{
    using MunCode.Core.Design.Domain;
    using MunCode.Core.Messaging.Endpoints.Filters.Caching;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Read;

    [Cached]
    public class GetOrderStatus : ValueObject<OrderStatus>, IRequest<OrderStatus>
    {
        public GetOrderStatus(OrderStatusEnum orderStatusId)
        {
            this.OrderStatusId = (byte)orderStatusId;
        }

        public byte OrderStatusId { get; }
    }
}