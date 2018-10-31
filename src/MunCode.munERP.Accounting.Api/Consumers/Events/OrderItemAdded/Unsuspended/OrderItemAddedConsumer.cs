namespace MunCode.munERP.Accounting.Api.Consumers.Events.OrderItemAdded.Unsuspended
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Domain;
    using MunCode.munERP.Accounting.Model.Messages.Events.OrderItemAdded;

    [ConsumerOfTopic("OrderItemUnsuspended")]
    public class OrderItemAddedConsumer : IEventConsumer<OrderItemAdded>
    {
        private readonly IRepository<CustomerBalance, int> repository;

        public OrderItemAddedConsumer(IRepository<CustomerBalance, int> repository)
        {
            Guard.NotNull(repository, nameof(repository));
            this.repository = repository;
        }

        public async Task Consume(ReceiveContext<OrderItemAdded> messageContext)
        {
            Guard.NotNull(messageContext, nameof(messageContext));
            var message = messageContext.Message;
            var customerBalance = await this.repository.Get(message.CustomerId);
            var receivable = Receivable.Create(message.OrderId, message.OrderTotal);
            customerBalance.BookReceivable(receivable);
            await this.repository.Save(customerBalance);
        }
    }
}