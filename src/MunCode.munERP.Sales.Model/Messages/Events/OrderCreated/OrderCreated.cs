namespace MunCode.munERP.Sales.Model.Messages.Events.OrderCreated
{
    using System;
    using System.Collections.Generic;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Read;

    using OrderItem = MunCode.munERP.Sales.Model.Messages.Events.OrderItem;

    public class OrderCreated : IEvent, IAggregateCreatedEvent, IHaveTopic
    {
        public OrderCreated(
            int customerId,
            Guid orderId,
            Money orderTotal,
            ICollection<OrderItem> items,
            OrderStatusEnum orderStatus)
        {
            Guard.NotNull(items, nameof(items));
            this.OrderId = orderId;
            this.OrderTotal = orderTotal;
            this.CustomerId = customerId;
            this.Items = items;
            this.OrderStatus = orderStatus;
        }

        public int CustomerId { get; }

        public Guid OrderId { get; }

        public Money OrderTotal { get; }

        public ICollection<OrderItem> Items { get; }

        public OrderStatusEnum OrderStatus { get; }

        public string Topic => this.OrderStatus.ToString();
    }
}