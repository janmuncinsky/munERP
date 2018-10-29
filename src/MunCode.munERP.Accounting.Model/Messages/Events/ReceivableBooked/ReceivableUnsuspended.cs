namespace MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked
{
    using System;

    using MunCode.Core.Design.Domain;

    public class ReceivableUnsuspended : ReceivableAccepted
    {
        public ReceivableUnsuspended(Guid orderId, Money amount, int customerBalanceId)
            : base(orderId, amount, customerBalanceId)
        {
        }
    }
}