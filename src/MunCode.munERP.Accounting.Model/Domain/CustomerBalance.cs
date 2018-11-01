namespace MunCode.munERP.Accounting.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.munERP.Accounting.Model.Messages.Events;
    using MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked;

    public class CustomerBalance : Aggregate<int>
    {
        private Money creditTotal;
        private Money receivableTotal;
        private HashSet<Receivable> receivables = new HashSet<Receivable>();

        protected CustomerBalance()
        {
        }

        private CustomerBalance(int id) : base(id)
        {
            this.creditTotal = Money.Default;
            this.receivableTotal = Money.Default;
        }

        private Money RemainingCredit => this.creditTotal - this.receivableTotal;

        public static CustomerBalance CreateCustomerBalance(int customerId)
        {
            var balance = new CustomerBalance(customerId);
            balance.RaiseEvent(new CustomerBalanceCreated(customerId));
            return balance;
        }

        public void BookReceivable(Receivable receivable)
        {
            Guard.NotNull(receivable, nameof(receivable));
            var bookingResult = receivable.Book(this.RemainingCredit);
            this.receivableTotal += bookingResult.Coverage;
            this.receivables.Add(receivable);
            this.RaiseEvent(new ReceivableBooked(bookingResult.OrderId, this.receivableTotal, this.Id, bookingResult.OrderStatus));
        }

        public void IncreaseReceivableAmount(Guid orderId, Money newAmount)
        {
            var receivable = this.receivables.Single(r => r.Id == orderId);
            this.receivableTotal -= receivable.IncreaseAmount(newAmount);
            var bookingResult = receivable.Book(this.RemainingCredit);
            this.receivableTotal += bookingResult.Coverage;
            this.RaiseEvent(new ReceivableBooked(bookingResult.OrderId, this.receivableTotal, this.Id, bookingResult.OrderStatus));
        }

        public void BookPayment(Guid orderId, Money paidAmount)
        {
            this.receivableTotal -= paidAmount;
            Receivable paidReceivable = null;
            var comparer = Receivable.ByCreatedAuditTrailComparerInstance;
            foreach (var receivable in this.receivables.OrderBy(o => o, comparer))
            {
                paidReceivable = receivable.TryBookPayment(orderId, paidAmount) ? receivable : paidReceivable;
                this.BookIfSuspended(receivable);
            }

            Guard.NotNull(paidReceivable, new InvalidOperationException("Cannot find requested receivable."));
            this.receivables.Remove(paidReceivable);
            this.RaiseEvent(new PaymentBooked(orderId));
        }

        public void ChangeCredit(Money newCreditTotal)
        {
            this.creditTotal = newCreditTotal;
            var comparer = Receivable.ByCreatedAuditTrailComparerInstance;

            foreach (var receivable in this.receivables.OrderBy(o => o, comparer))
            {
                this.BookIfSuspended(receivable);
            }
        }

        private void BookIfSuspended(Receivable receivable)
        {
            var bookingResult = receivable.BookIfSuspended(this.RemainingCredit);
            if (bookingResult != ReceivableBookingResult.EmptyResult)
            {
                this.receivableTotal += bookingResult.Coverage;
                this.RaiseEvent(new ReceivableBooked(bookingResult.OrderId, this.receivableTotal, this.Id, bookingResult.OrderStatus));
            }
        }
    }
}