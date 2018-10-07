namespace MunCode.munERP.Accounting.Model.Messages.Events
{
    using MunCode.Core.Messaging.Messages;

    public class CustomerCreated : IEvent
    {
        public CustomerCreated(int customerId)
        {
            this.CustomerId = customerId;
        }

        public int CustomerId { get; }
    }
}