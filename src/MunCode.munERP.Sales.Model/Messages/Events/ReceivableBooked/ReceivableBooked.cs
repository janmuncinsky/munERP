namespace MunCode.munERP.Sales.Model.Messages.Events.ReceivableBooked
{
    using System;

    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Read;

    public class ReceivableBooked : IEvent
    {
        public ReceivableBooked(Guid orderId, OrderStatusEnum orderStatus)
        {
            this.OrderId = orderId;
            this.OrderStatus = orderStatus;
        }

        public Guid OrderId { get; }

        public OrderStatusEnum OrderStatus { get; }
    }
}