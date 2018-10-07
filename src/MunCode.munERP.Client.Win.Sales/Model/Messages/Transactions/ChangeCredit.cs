namespace MunCode.munERP.Client.Win.Sales.Model.Messages.Transactions
{
    using System;

    using MunCode.Core.Messaging.Messages;

    public class ChangeCredit : ICommand
    {
        public ChangeCredit(Guid customerId, decimal creditTotal, string currency)
        {
            this.CustomerId = customerId;
            this.CreditTotal = creditTotal;
            this.Currency = currency;
        }

        public Guid CustomerId { get; }

        public decimal CreditTotal { get; }

        public string Currency { get; }
    }
}