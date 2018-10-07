namespace MunCode.munERP.Sales.Model.Messages.Commands
{
    using System;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Responses;

    public class AddOrderItem : ITransaction<OrderStatusResponse>
    {
        public AddOrderItem(Guid orderId, OrderItem orderItem)
        {
            Guard.NotNull(orderItem, nameof(orderItem));
            this.OrderId = orderId;
            this.OrderItem = orderItem;
        }

        public Guid OrderId { get; }

        public OrderItem OrderItem { get; }
    }
}