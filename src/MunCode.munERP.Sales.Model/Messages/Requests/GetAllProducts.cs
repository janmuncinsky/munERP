namespace MunCode.munERP.Sales.Model.Messages.Requests
{
    using MunCode.Core.Messaging.Endpoints.Filters.Caching;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Read;

    [Cached]
    public class GetAllProducts : Request<GetAllProducts, Product[]>
    {
    }
}