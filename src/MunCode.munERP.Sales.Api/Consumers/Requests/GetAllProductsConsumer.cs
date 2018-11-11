namespace MunCode.munERP.Sales.Api.Consumers.Requests
{

    using Microsoft.EntityFrameworkCore;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Read;

    public class GetAllProductsConsumer : GetAllConsumer<GetAllProducts, Product>
    {
        public GetAllProductsConsumer(DbContext context) : base(context)
        {
        }
    }
}