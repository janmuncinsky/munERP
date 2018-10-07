namespace MunCode.mERP.Accounting.Model.Messages.Events.ReceivableBooked
{
    using System;

    using MunCode.Core.Messaging.Messages;

    public abstract class ReceivableBooked : IEvent
    {
        protected ReceivableBooked(Guid orderId)
        {
            this.OrderId = orderId;
        }

        public Guid OrderId { get; }
    }
}