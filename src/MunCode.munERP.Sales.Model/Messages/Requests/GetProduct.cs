namespace MunCode.munERP.Sales.Model.Messages.Requests
{
    using MunCode.Core.Design.Domain;
    using MunCode.Core.Messaging.Endpoints.Filters.Caching;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Read;

    [Cached]
    public class GetProduct : ValueObject<GetProduct>, IRequest<Product>
    {
        public GetProduct(int productId)
        {
            this.ProductId = productId;
        }

        public int ProductId { get; }
    }
}