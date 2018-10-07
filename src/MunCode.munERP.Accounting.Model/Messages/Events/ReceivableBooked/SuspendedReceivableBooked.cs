namespace MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked
{
    using System;

    public class SuspendedReceivableBooked : ReceivableBooked
    {
        public SuspendedReceivableBooked(Guid orderId)
            : base(orderId)
        {
        }
    }
}