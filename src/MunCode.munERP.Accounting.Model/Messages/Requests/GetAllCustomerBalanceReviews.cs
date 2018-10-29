namespace MunCode.munERP.Accounting.Model.Messages.Requests
{
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Read;

    public class GetAllCustomerBalanceReviews : Request<GetAllCustomerBalanceReviews, CustomerBalanceReview[]>
    {
    }
}