namespace MunCode.mERP.Sales.Api.Consumers.Requests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Messages.Requests;
    using MunCode.mERP.Sales.Model.Read;

    public class GetProductConsumer : IRequestConsumer<GetProduct, Product>
    {
        private readonly DbContext context;

        public GetProductConsumer(DbContext context)
        {
            Guard.NotNull(context, nameof(context));
            this.context = context;
        }

        public Task<Product> Consume(ReceiveContext<GetProduct> messageContext)
        {
            Guard.NotNull(messageContext, nameof(messageContext));
            return this.context
                .Set<Product>()
                .SingleAsync(p => p.Id == messageContext.Message.ProductId);
        }
    }
}