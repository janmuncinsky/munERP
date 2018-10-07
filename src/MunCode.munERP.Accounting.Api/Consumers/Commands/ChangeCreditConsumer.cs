namespace MunCode.munERP.Accounting.Api.Consumers.Commands
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Domain;
    using MunCode.munERP.Accounting.Model.Messages.Commands;

    public class ChangeCreditConsumer : ICommandConsumer<ChangeCredit>
    {
        private readonly IRepository<CustomerBalance, int> repository;

        public ChangeCreditConsumer(IRepository<CustomerBalance, int> repository)
        {
            Guard.NotNull(repository, nameof(repository));
            this.repository = repository;
        }

        public async Task Consume(ReceiveContext<ChangeCredit> messageContext)
        {
            Guard.NotNull(messageContext, nameof(messageContext));
            var message = messageContext.Message;
            var customerBalance = await this.repository.Get(message.CustomerId);
            customerBalance.ChangeCredit(message.CreditTotal);
            await this.repository.Save(customerBalance);
        }
    }
}