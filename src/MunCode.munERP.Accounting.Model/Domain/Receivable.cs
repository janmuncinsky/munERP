namespace MunCode.munERP.Accounting.Model.Domain
{
    using System;
    using System.Collections.Generic;

    using MunCode.Core.Design.Domain;
    using MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked;
    using MunCode.munERP.Accounting.Model.Read;

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

        public ReceivableBookingResult Book(Money remainingCredit)
        {
            var coverage = this.BookInternal(remainingCredit);
            var status = OrderStatusEnum.ReceivableAccepted;
            if (coverage == Money.Default)
            {
                status = OrderStatusEnum.ReceivableSuspended;
            }

            return new ReceivableBookingResult(this.Id, coverage, status);
        }

        public ReceivableBookingResult BookIfSuspended(Money remainingCredit)
        {
            if (this.isSuspended)
            {
                return this.Book(remainingCredit);
            }

            return ReceivableBookingResult.EmptyResult;
        }

        protected virtual Money BookInternal(Money remainingCredit)
        {
            if (this.amount <= remainingCredit)
            {
                this.isSuspended = false;
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