namespace MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked
{
    using System;

    using MunCode.Core.Design.Domain;

    public class ReceivableAccepted : ReceivableBooked
    {
        public ReceivableAccepted(Guid orderId, Money amount, int customerBalanceId)
            : base(orderId)
        {
            this.Amount = amount;
            this.CustomerBalanceId = customerBalanceId;
        }

        public Money Amount { get; }

        public int CustomerBalanceId { get; }
    }
}