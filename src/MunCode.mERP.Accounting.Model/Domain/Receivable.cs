namespace MunCode.mERP.Accounting.Model.Domain
{
    using System;
    using System.Collections.Generic;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Accounting.Model.Messages.Events.ReceivableBooked;

    public class Receivable : Entity<Guid>
    {
        private Money amount;
        private AuditTrail auditTrail;
        private bool isSuspended;

        protected Receivable()
        {
        }

        private Receivable(Guid id, Money amount) : base(id)
        {
            this.auditTrail = AuditTrail.GetCurrent();
            this.isSuspended = true;
            this.amount = amount;
        }

        public static Comparer<Receivable> ByCreatedAuditTrailComparerInstance { get; } = new ByCreatedAuditTrailComparer();

        public static Receivable Create(Guid orderId, Money amount)
        {
            return new Receivable(orderId, amount);
        }

        public bool TryBookPayment(Guid orderId, Money paidAmount)
        {
            if (this.Id == orderId)
            {
                if (this.isSuspended)
                {
                    throw new InvalidOperationException("Cannot pay for suspended order.");
                }

                if (paidAmount != this.amount)
                {
                    throw new InvalidOperationException("Payment amount doesn't match the receivable amount.");
                }

                return true;
            }

            return false;
        }

        public Money IncreaseAmount(Money newAmount)
        {
            this.isSuspended = true;
            var oldAmount = this.amount;
            this.amount = newAmount;
            return oldAmount;
        }

        public Money GetCreditCoverage(Money remainingCredit)
        {
            var coverage = this.GetCreditCoverageInternal(remainingCredit, () => new AcceptedReceivableBooked(this.Id));
            if (coverage == Money.Default)
            {
                this.RaiseEvent(new SuspendedReceivableBooked(this.Id));
            }

            return coverage;
        }

        public Money GetCreditCoverageWhenSuspended(Money remainingCredit)
        {
            if (this.isSuspended)
            {
                return this.GetCreditCoverageInternal(remainingCredit, () => new UnsuspendedReceivableBooked(this.Id));
            }

            return Money.Default;
        }

        protected virtual Money GetCreditCoverageInternal<TValidEvent>(Money remainingCredit, Func<TValidEvent> validEventFactory)
            where TValidEvent : IEvent
        {
            if (this.amount <= remainingCredit)
            {
                this.isSuspended = false;
                this.RaiseEvent(validEventFactory());
                return this.amount;
            }

            return Money.Default;
        }

        private sealed class ByCreatedAuditTrailComparer : Comparer<Receivable>
        {
            public override int Compare(Receivable x, Receivable y)
            {
                if (ReferenceEquals(x, y))
                {
                    return 0;
                }

                if (ReferenceEquals(null, y))
                {
                    return 1;
                }

                if (ReferenceEquals(null, x))
                {
                    return -1;
                }

                return AuditTrail.ByCreatedComparerInstance.Compare(x.auditTrail, y.auditTrail);
            }
        }
    }
}