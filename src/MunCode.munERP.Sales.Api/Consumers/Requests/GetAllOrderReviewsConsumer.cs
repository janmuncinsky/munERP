namespace MunCode.munERP.Sales.Api.Consumers.Requests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Read;

    public class GetAllOrderReviewsConsumer : IRequestConsumer<GetAllOrderReviews, OrderReview[]>
    {
        private readonly DbContext context;

        public GetAllOrderReviewsConsumer(DbContext context)
        {
            Guard.NotNull(context, nameof(context));
            this.context = context;
        }

        public Task<OrderReview[]> Consume(ReceiveContext<GetAllOrderReviews> messageContext)
        {
            return this.context.Set<OrderReview>().ToArrayAsync();
        }
    }
}