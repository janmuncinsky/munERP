namespace MunCode.mERP.Sales.Model.Messages.Requests
{
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Read;

    public class GetAllOrderReviews : Request<GetAllOrderReviews, OrderReview[]>
    {
    }
}