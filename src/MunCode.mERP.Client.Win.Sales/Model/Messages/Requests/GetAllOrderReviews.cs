namespace MunCode.mERP.Client.Win.Sales.Model.Messages.Requests
{
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Client.Win.Sales.Model.Read;
    using MunCode.mERP.Client.Win.Sales.UI.OrderReview;

    [BusyNotifierMessage(typeof(OrderReviewViewModel))]
    public class GetAllOrderReviews : Request<GetAllOrderReviews, OrderReview[]>
    {
    }
}