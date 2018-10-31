namespace MunCode.munERP.Sales.Api.Consumers.Events.OrderItemAdded
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Events.OrderItemAdded;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Messages.Responses;
    using MunCode.munERP.Sales.Model.Read;

    using OrderItem = MunCode.munERP.Sales.Model.Read.OrderItem;

    public class OrderItemAddedConsumer : IEventConsumer<OrderItemAdded>
    {
        private readonly IUnitOfWork uow;
        private readonly IRequestBus requestBus;

        public OrderItemAddedConsumer(IUnitOfWork uow, IMessageBus requestBus)
        {
            Guard.NotNull(requestBus, nameof(requestBus));
            Guard.NotNull(uow, nameof(uow));
            this.uow = uow;
            this.requestBus = requestBus;
        }

        public virtual async Task Consume(ReceiveContext<OrderItemAdded> messageContext)
        {
            var message = messageContext.Message;
            var orderStatus = await this.GetOrderStatus(message.OrderStatus);
            this.uow.Update<OrderReview>()
                .SetReference(o => o.OrderTotal, message.OrderTotal)
                .SetValue(o => o.OrderStatusDescription, orderStatus.Description)
                .Where(o => o.Id, message.OrderId);

            var request = new GetProduct(message.OrderItem.ProductId);
            var product = await this.requestBus.Request<GetProduct, Product>(request);

            var item = new OrderItem(
                message.OrderId,
                message.OrderItem.LineNumber,
                product.Name,
                message.OrderItem.Quantity,
                message.OrderItem.Price);

            this.uow.Add(item);

            await this.uow.Save();

            if (message.OrderStatus == OrderStatusEnum.OrderSuspended)
            {
                var response = new OrderStatusResponse(orderStatus.LongDescription);
                await messageContext.Respond(response);
            }
        }

        protected Task<OrderStatus> GetOrderStatus(OrderStatusEnum orderStatus)
        {
            var orderStatusRequest = new GetOrderStatus(orderStatus);
            return this.requestBus.Request<GetOrderStatus, OrderStatus>(orderStatusRequest);
        }
    }
}