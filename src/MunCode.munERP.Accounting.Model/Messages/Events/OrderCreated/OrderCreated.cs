namespace MunCode.munERP.Accounting.Model.Messages.Events.OrderCreated
{
    using System;
    using System.Collections.Generic;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Messages.Events;

    public class OrderCreated : IEvent
    {
        public OrderCreated(
            int customerId,
            Guid orderId,
            Money orderTotal,
            ICollection<OrderItem> items)
        {
            Guard.NotNull(items, nameof(items));
            this.OrderId = orderId;
            this.OrderTotal = orderTotal;
            this.CustomerId = customerId;
            this.Items = items;
        }

        public int CustomerId { get; }

        public Guid OrderId { get; }

        public Money OrderTotal { get; }

        public ICollection<OrderItem> Items { get; }
    }
}