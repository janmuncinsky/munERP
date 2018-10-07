namespace MunCode.munERP.Sales.Api.Consumers.Requests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Read;

    public class GetAllProductsConsumer : IRequestConsumer<GetAllProducts, Product[]>
    {
        private readonly DbContext context;

        public GetAllProductsConsumer(DbContext context)
        {
            Guard.NotNull(context, nameof(context));
            this.context = context;
        }

        public Task<Product[]> Consume(ReceiveContext<GetAllProducts> messageContext)
        {
            return this.context.Set<Product>().ToArrayAsync();
        }
    }
}