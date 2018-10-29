namespace MunCode.munERP.Sales.Model.Messages.Events.ReceivableBooked
{
    using System;

    public class ReceivableAccepted : ReceivableBooked
    {
        public ReceivableAccepted(Guid orderId)
            : base(orderId)
        {
        }
    }
}