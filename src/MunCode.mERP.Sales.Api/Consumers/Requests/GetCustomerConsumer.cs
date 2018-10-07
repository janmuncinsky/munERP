namespace MunCode.mERP.Sales.Api.Consumers.Requests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Messages.Requests;
    using MunCode.mERP.Sales.Model.Read;

    public class GetCustomerConsumer : IRequestConsumer<GetCustomer, Customer>
    {
        private readonly DbContext context;

        public GetCustomerConsumer(DbContext context)
        {
            Guard.NotNull(context, nameof(context));
            this.context = context;
        }

        public Task<Customer> Consume(ReceiveContext<GetCustomer> messageContext)
        {
            Guard.NotNull(messageContext, nameof(messageContext));
            return this.context
                .Set<Customer>()
                .SingleAsync(c => c.Id == messageContext.Message.CustomerId);
        }
    }
}