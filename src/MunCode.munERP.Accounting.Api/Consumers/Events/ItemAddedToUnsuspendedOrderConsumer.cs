namespace MunCode.munERP.Accounting.Api.Consumers.Events
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Domain;
    using MunCode.munERP.Accounting.Model.Messages.Events.OrderItemAdded;

    public class ItemAddedToUnsuspendedOrderConsumer : IEventConsumer<ItemAddedToUnsuspendedOrder>
    {
        private readonly IRepository<CustomerBalance, int> repository;

        public ItemAddedToUnsuspendedOrderConsumer(IRepository<CustomerBalance, int> repository)
        {
            Guard.NotNull(repository, nameof(repository));
            this.repository = repository;
        }

        public async Task Consume(ReceiveContext<ItemAddedToUnsuspendedOrder> messageContext)
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