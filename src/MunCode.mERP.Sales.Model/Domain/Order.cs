namespace MunCode.mERP.Sales.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Messages.Events.OrderCreated;
    using MunCode.mERP.Sales.Model.Messages.Events.OrderItemAdded;

    using OrderItem = MunCode.mERP.Sales.Model.Messages.Events.OrderItem;

    public class Order : Aggregate<Guid>
    {
        private static readonly Money MinimalOrderLimit = new Money(500, "USD");
        private Money orderTotal;
        private int customerId;
        private bool isSuspended;

        protected Order()
        {
        }

        private Order(Guid id, int customerId, Money orderTotal) : base(id)
        {
            this.customerId = customerId;
            this.orderTotal = orderTotal;
        }

        public static Order CreateOrder(int customerId, ICollection<OrderItem> items)
        {
            Guard.NotEmpty(items, nameof(items));
            var orderTotal = SumAmount(Money.Default, items);
            var order = new Order(Guid.NewGuid(), customerId, orderTotal);
            var data = new OrderCreatedData(customerId, order.Id, orderTotal, items);
            order.CheckStatus<OrderCreated>(
                () => new AcceptedOrderCreated(data),
                () => new SuspendedOrderCreated(data));
            return order;
        }

        public void AddItem(OrderItem item)
        {
            Guard.NotNull(item, nameof(item));
            bool wasSuspended = this.isSuspended;
            this.orderTotal = SumAmount(this.orderTotal, new[] { item });
            var data = new OrderItemAddedData(this.Id, this.customerId, this.orderTotal, item);
            this.CheckStatus(
                () => wasSuspended ? (OrderItemAdded)new ItemAddedToUnsuspendedOrder(data) : new ItemAddedToAcceptedOrder(data), 
                () => new ItemAddedToSuspendedOrder(data));
        }

        protected void CheckStatus<TEvent>(
            Func<TEvent> acceptedEventFactory,
            Func<TEvent> suspendedEventFactory)
            where TEvent : IEvent
        {
            this.isSuspended = this.orderTotal < MinimalOrderLimit;
            var @event = this.isSuspended ? (IEvent)suspendedEventFactory() : acceptedEventFactory();
            this.RaiseEvent(@event);
        }

        private static Money SumAmount(Money currentAmount, ICollection<OrderItem> items)
        {
            return items.Aggregate(currentAmount, (current, item) => current + (item.Price * item.Quantity));
        }
    }
}
