namespace MunCode.munERP.Accounting.Api.Consumers.Requests
{

    using Microsoft.EntityFrameworkCore;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.munERP.Accounting.Model.Messages.Requests;
    using MunCode.munERP.Accounting.Model.Read;

    public class GetAllCustomerBalanceReviewsConsumer : GetAllConsumer<GetAllCustomerBalanceReviews, CustomerBalanceReview>
    {
        public GetAllCustomerBalanceReviewsConsumer(DbContext context) : base(context)
        {
        }
    }
}