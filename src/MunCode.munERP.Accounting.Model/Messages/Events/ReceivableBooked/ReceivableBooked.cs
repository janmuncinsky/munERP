namespace MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked
{
    using System;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Read;

    public class ReceivableBooked : IEvent, IHaveTopic
    {
        public ReceivableBooked(Guid orderId, Money receivableTotal, int customerBalanceId, OrderStatusEnum orderStatus)
        {
            this.OrderId = orderId;
            this.ReceivableTotal = receivableTotal;
            this.CustomerBalanceId = customerBalanceId;
            this.OrderStatus = orderStatus;
            this.Topic = orderStatus.ToString();
        }

        public Guid OrderId { get; }

        public Money ReceivableTotal { get; }

        public int CustomerBalanceId { get; }

        public OrderStatusEnum OrderStatus { get; }

        public string Topic { get; }
    }
}