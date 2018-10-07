namespace MunCode.mERP.Accounting.Model.Messages.Events.OrderItemAdded
{
    using System;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;

    public class OrderItemAddedData
    {
        public OrderItemAddedData(
            Guid orderId,
            int customerId,
            Money orderTotal,
            OrderItem orderItem)
        {
            Guard.NotNull(orderItem, nameof(orderItem));
            this.OrderId = orderId;
            this.CustomerId = customerId;
            this.OrderTotal = orderTotal;
            this.OrderItem = orderItem;
        }

        public Guid OrderId { get; }

        public int CustomerId { get; }

        public Money OrderTotal { get; }

        public OrderItem OrderItem { get; }
    }
}