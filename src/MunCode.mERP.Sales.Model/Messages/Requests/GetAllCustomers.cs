namespace MunCode.mERP.Sales.Model.Messages.Requests
{
    using MunCode.Core.Messaging.Endpoints.Filters.Caching;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Read;

    [Cached]
    public class GetAllCustomers : Request<GetAllCustomers, Customer[]>
    {
    }
}