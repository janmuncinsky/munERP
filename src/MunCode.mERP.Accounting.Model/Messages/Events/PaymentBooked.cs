namespace MunCode.mERP.Accounting.Model.Messages.Events
{
    using System;

    using MunCode.Core.Messaging.Messages;

    public class PaymentBooked : IEvent
    {
        public PaymentBooked(Guid businessCaseId)
        {
            this.BusinessCaseId = businessCaseId;
        }

        public Guid BusinessCaseId { get; }
    }
}