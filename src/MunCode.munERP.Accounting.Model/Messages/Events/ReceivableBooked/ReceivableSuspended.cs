namespace MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked
{
    using System;

    public class ReceivableSuspended : ReceivableBooked
    {
        public ReceivableSuspended(Guid orderId)
            : base(orderId)
        {
        }
    }
}