namespace MunCode.munERP.Client.Win.Sales.Model.Messages.Requests
{
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Client.Win.Sales.Model.Read;
    using MunCode.munERP.Client.Win.Sales.UI.Documents;

    [BusyNotifierMessage(typeof(OrderReviewViewModel))]
    public class GetAllOrderReviews : Request<GetAllOrderReviews, OrderReview[]>
    {
    }
}