namespace MunCode.munERP.Accounting.Model.Messages.Commands
{
    using System;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Messaging.Messages;

    public class BookPayment : ICommand
    {
        public BookPayment(int customerId, Guid orderId, Money paidAmount)
        {
            this.OrderId = orderId;
            this.PaidAmount = paidAmount;
            this.CustomerId = customerId;
        }

        public Guid OrderId { get; }

        public Money PaidAmount { get; }

        public int CustomerId { get; }
    }
}
