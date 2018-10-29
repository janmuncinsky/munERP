namespace MunCode.munERP.Sales.Api.Consumers.Requests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Read;

    public class GetAllCustomersConsumer : IRequestConsumer<GetAllCustomers, Customer[]>
    {
        private readonly DbContext context;

        public GetAllCustomersConsumer(DbContext context)
        {
            Guard.NotNull(context, nameof(context));
            this.context = context;
        }

        public Task<Customer[]> Consume(ReceiveContext<GetAllCustomers> messageContext)
        {
            return this.context.Set<Customer>().ToArrayAsync();
        }
    }
}