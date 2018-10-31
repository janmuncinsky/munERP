namespace MunCode.munERP.Sales.Api.Consumers.Events.OrderCreated
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Events.OrderCreated;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Messages.Responses;
    using MunCode.munERP.Sales.Model.Read;

    using OrderItem = MunCode.munERP.Sales.Model.Read.OrderItem;

    public class OrderCreatedConsumer : IEventConsumer<OrderCreated>
    {
        private readonly IUnitOfWork uow;
        private readonly IRequestBus requestBus;

        public OrderCreatedConsumer(IUnitOfWork uow, IMessageBus requestBus)
        {
            Guard.NotNull(requestBus, nameof(requestBus));
            Guard.NotNull(uow, nameof(uow));
            this.uow = uow;
            this.requestBus = requestBus;
        }

        public virtual async Task Consume(ReceiveContext<OrderCreated> messageContext)
        {
            var message = messageContext.Message;
            var customerRequest = new GetCustomer(message.CustomerId);
            var customer = await this.requestBus.Request<GetCustomer, Customer>(customerRequest);
            var orderStatus = await this.GetOrderStatus(message.OrderStatus);

            var orderReview = new OrderReview(
                message.OrderId,
                customer.Name,
                message.OrderTotal,
                orderStatus.Description);
            this.uow.Add(orderReview);

            foreach (var eventItem in message.Items)
            {
                var productRequest = new GetProduct(eventItem.ProductId);
                var product = await this.requestBus.Request<GetProduct, Product>(productRequest);

                var item = new OrderItem(
                    message.OrderId,
                    eventItem.LineNumber,
                    product.Name,
                    eventItem.Quantity,
                    eventItem.Price);
                this.uow.Add(item);
            }

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