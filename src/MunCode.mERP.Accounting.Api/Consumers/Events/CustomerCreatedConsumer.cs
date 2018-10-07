namespace MunCode.mERP.Accounting.Api.Consumers.Events
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Accounting.Model.Domain;
    using MunCode.mERP.Accounting.Model.Messages.Events;

    public class CustomerCreatedConsumer : IEventConsumer<CustomerCreated>
    {
        private readonly IRepository<CustomerBalance, int> repository;

        public CustomerCreatedConsumer(IRepository<CustomerBalance, int> repository)
        {
            Guard.NotNull(repository, nameof(repository));
            this.repository = repository;
        }

        public async Task Consume(ReceiveContext<CustomerCreated> messageContext)
        {
            Guard.NotNull(messageContext, nameof(messageContext));
            var customerBalance = CustomerBalance.CreateCustomerBalance(messageContext.Message.CustomerId);
            await this.repository.Save(customerBalance);
        }
    }
}