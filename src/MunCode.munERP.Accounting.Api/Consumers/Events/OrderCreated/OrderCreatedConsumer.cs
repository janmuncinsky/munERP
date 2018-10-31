namespace MunCode.munERP.Accounting.Api.Consumers.Events.OrderCreated
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Domain;
    using MunCode.munERP.Accounting.Model.Messages.Events.OrderCreated;

    [ConsumerOfTopic("OrderAccepted")]
    public class OrderCreatedConsumer : IEventConsumer<OrderCreated>
    {
        private readonly IRepository<CustomerBalance, int> repository;

        public OrderCreatedConsumer(IRepository<CustomerBalance, int> repository)
        {
            Guard.NotNull(repository, nameof(repository));
            this.repository = repository;
        }

        public async Task Consume(ReceiveContext<OrderCreated> messageContext)
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
