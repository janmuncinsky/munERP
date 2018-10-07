namespace MunCode.munERP.Sales.Model.Messages.Requests
{
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Read;

    public class GetAllOrderReviews : Request<GetAllOrderReviews, OrderReview[]>
    {
    }
}