namespace MunCode.munERP.Accounting.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.munERP.Accounting.Model.Messages.Events;

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
            this.receivableTotal += receivable.TryGetCreditCoverage(this.RemainingCredit, this.Id);
            this.receivables.Add(receivable);
        }

        public void IncreaseReceivableAmount(Guid orderId, Money newAmount)
        {
            var receivable = this.receivables.Single(r => r.Id == orderId);
            this.receivableTotal -= receivable.IncreaseAmount(newAmount);
            this.receivableTotal += receivable.TryGetCreditCoverage(this.RemainingCredit, this.Id);
        }

        public void BookPayment(Guid orderId, Money paidAmount)
        {
            this.receivableTotal -= paidAmount;
            Receivable paidReceivable = null;
            var comparer = Receivable.ByCreatedAuditTrailComparerInstance;
            foreach (var receivable in this.receivables.OrderBy(o => o, comparer))
            {
                paidReceivable = receivable.TryBookPayment(orderId, paidAmount) ? receivable : paidReceivable;
                this.receivableTotal += receivable.GetCreditCoverageWhenSuspended(this.RemainingCredit, this.Id);
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
                this.receivableTotal += receivable.GetCreditCoverageWhenSuspended(this.RemainingCredit, this.Id);
            }
        }
    }
}