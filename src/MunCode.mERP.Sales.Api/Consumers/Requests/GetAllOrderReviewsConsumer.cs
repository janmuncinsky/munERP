namespace MunCode.mERP.Sales.Api.Consumers.Requests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Messages.Requests;
    using MunCode.mERP.Sales.Model.Read;

    public class GetAllOrderReviewsConsumer : IRequestConsumer<GetAllOrderReviews, OrderReview[]>
    {
        private readonly DbContext context;

        public GetAllOrderReviewsConsumer(DbContext context)
        {
            this.context = context;
        }

        public async Task<OrderReview[]> Consume(ReceiveContext<GetAllOrderReviews> messageContext)
        {
            return await this.context.Set<OrderReview>().ToArrayAsync();
        }
    }
}