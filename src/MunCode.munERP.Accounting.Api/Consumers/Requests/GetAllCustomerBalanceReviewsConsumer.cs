namespace MunCode.munERP.Accounting.Api.Consumers.Requests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Messages.Requests;
    using MunCode.munERP.Accounting.Model.Read;

    public class GetAllCustomerBalanceReviewsConsumer : IRequestConsumer<GetAllCustomerBalanceReviews, CustomerBalanceReview[]>
    {
        private readonly DbContext context;

        public GetAllCustomerBalanceReviewsConsumer(DbContext context)
        {
            Guard.NotNull(context, nameof(context));
            this.context = context;
        }

        public Task<CustomerBalanceReview[]> Consume(ReceiveContext<GetAllCustomerBalanceReviews> messageContext)
        {
            return this.context.Set<CustomerBalanceReview>().ToArrayAsync();
        }
    }
}