namespace MunCode.munERP.Sales.Api.Consumers.Requests
{

    using Microsoft.EntityFrameworkCore;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Read;

    public class GetAllOrderReviewsConsumer : GetAllConsumer<GetAllOrderReviews, OrderReview>
    {
        public GetAllOrderReviewsConsumer(DbContext context) : base(context)
        {
        }
    }
}