namespace MunCode.munERP.Sales.Api.Consumers.Requests
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Read;

    public class GetOrderStatusConsumer : IRequestConsumer<GetOrderStatus, OrderStatus>
    {
        private readonly DbContext context;

        public GetOrderStatusConsumer(DbContext context)
        {
            Guard.NotNull(context, nameof(context));
            this.context = context;
        }

        public Task<OrderStatus> Consume(ReceiveContext<GetOrderStatus> messageContext)
        {
            Guard.NotNull(messageContext, nameof(messageContext));
            return this.context
                .Set<OrderStatus>()
                .SingleAsync(p => p.Id == messageContext.Message.OrderStatusId);
        }
    }
}