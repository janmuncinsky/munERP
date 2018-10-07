namespace MunCode.mERP.Sales.Model.Messages.Events.ReceivableBooked
{
    using System;

    public class AcceptedReceivableBooked : ReceivableBooked
    {
        public AcceptedReceivableBooked(Guid orderId)
            : base(orderId)
        {
        }
    }
}