namespace MunCode.munERP.Sales.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.munERP.Sales.Model.Messages.Events.OrderCreated;
    using MunCode.munERP.Sales.Model.Messages.Events.OrderItemAdded;
    using MunCode.munERP.Sales.Model.Read;

    using OrderItem = MunCode.munERP.Sales.Model.Messages.Events.OrderItem;

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
            order.CheckStatus();
            var status = order.isSuspended ? OrderStatusEnum.OrderSuspended : OrderStatusEnum.OrderAccepted;
            var orderCreated = new OrderCreated(customerId, order.Id, orderTotal, items, status);
            order.RaiseEvent(orderCreated);
            return order;
        }

        public void AddItem(OrderItem item)
        {
            Guard.NotNull(item, nameof(item));
            bool wasSuspended = this.isSuspended;
            this.orderTotal = SumAmount(this.orderTotal, new[] { item });
            this.CheckStatus();
            var orderStatus = OrderStatusEnum.OrderSuspended;
            var itemStatus = OrderItemAdded.OrderItemStatusEnum.OrderItemSuspended;
            if (!this.isSuspended)
            {
                orderStatus = OrderStatusEnum.OrderAccepted;
                itemStatus = wasSuspended
                        ? OrderItemAdded.OrderItemStatusEnum.OrderItemUnsuspended
                        : OrderItemAdded.OrderItemStatusEnum.OrderItemAccepted;
            }

            var orderItemAdded = new OrderItemAdded(this.Id, this.customerId, this.orderTotal, item, orderStatus, itemStatus);
            this.RaiseEvent(orderItemAdded);
        }

        protected void CheckStatus()
        {
            this.isSuspended = this.orderTotal < MinimalOrderLimit;
        }

        private static Money SumAmount(Money currentAmount, ICollection<OrderItem> items)
        {
            return items.Aggregate(currentAmount, (current, item) => current + (item.Price * item.Quantity));
        }
    }
}
