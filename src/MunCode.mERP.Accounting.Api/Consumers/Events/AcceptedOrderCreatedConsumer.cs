namespace MunCode.mERP.Accounting.Api.Consumers.Events
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Accounting.Model.Domain;
    using MunCode.mERP.Accounting.Model.Messages.Events.OrderCreated;

    public class AcceptedOrderCreatedConsumer : IEventConsumer<AcceptedOrderCreated>
    {
        private readonly IRepository<CustomerBalance, int> repository;

        public AcceptedOrderCreatedConsumer(IRepository<CustomerBalance, int> repository)
        {
            Guard.NotNull(repository, nameof(repository));
            this.repository = repository;
        }

        public async Task Consume(ReceiveContext<AcceptedOrderCreated> messageContext)
        {
            Guard.NotNull(messageContext, nameof(messageContext));
            var messageData = messageContext.Message.Data;
            var customerBalance = await this.repository.Get(messageData.CustomerId);
            var receivable = Receivable.Create(messageData.OrderId, messageData.OrderTotal);
            customerBalance.BookReceivable(receivable);
            await this.repository.Save(customerBalance);
        }
    }
}
