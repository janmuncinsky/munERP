namespace MunCode.munERP.Sales.Model.Messages.Events.OrderItemAdded
{
    using System;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Read;

    using OrderItem = MunCode.munERP.Sales.Model.Messages.Events.OrderItem;

    public class OrderItemAdded : IEvent, IHaveTopic
    {
        public OrderItemAdded(
            Guid orderId,
            int customerId,
            Money orderTotal,
            OrderItem orderItem,
            Read.OrderStatusEnum orderStatus,
            OrderStatusEnum itemOrderStatus)
        {
            Guard.NotNull(orderItem, nameof(orderItem));
            this.OrderId = orderId;
            this.CustomerId = customerId;
            this.OrderTotal = orderTotal;
            this.OrderItem = orderItem;
            this.OrderStatus = orderStatus;
            this.Topic = itemOrderStatus.ToString();
        }

        public enum OrderStatusEnum
        {
            OrderUnsuspended,
            OrderAccepted,
            OrderSuspended
        }

        public Guid OrderId { get; }

        public int CustomerId { get; }

        public Money OrderTotal { get; }

        public OrderItem OrderItem { get; }

        public Read.OrderStatusEnum OrderStatus { get; }

        public string Topic { get; }
    }
}