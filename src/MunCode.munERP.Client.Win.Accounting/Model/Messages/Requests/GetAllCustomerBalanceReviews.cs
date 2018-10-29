namespace MunCode.munERP.Client.Win.Accounting.Model.Messages.Requests
{
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Client.Win.Accounting.Model.Read;
    using MunCode.munERP.Client.Win.Accounting.UI.CustomerBalance;

    [BusyNotifierMessage(typeof(CustomerBalanceReviewViewModel))]
    public class GetAllCustomerBalanceReviews : Request<GetAllCustomerBalanceReviews, CustomerBalanceReview[]>
    {
    }
}