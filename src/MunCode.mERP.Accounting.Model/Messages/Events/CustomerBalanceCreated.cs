namespace MunCode.mERP.Accounting.Model.Messages.Events
{
    using MunCode.Core.Design.Domain;
    using MunCode.Core.Messaging.Messages;

    public class CustomerBalanceCreated : IEvent, IAggregateCreatedEvent
    {
        public CustomerBalanceCreated(int customerId)
        {
            this.CustomerId = customerId;
        }

        public int CustomerId { get; }
    }
}