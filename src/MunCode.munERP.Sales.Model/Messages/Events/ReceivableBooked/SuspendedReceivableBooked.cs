namespace MunCode.munERP.Sales.Model.Messages.Events.ReceivableBooked
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