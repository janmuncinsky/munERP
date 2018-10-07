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
    using MunCode.munERP.Sales.Model.Read;

    using OrderItem = MunCode.munERP.Sales.Model.Read.OrderItem;

    public abstract class OrderCreatedConsumer<TOrderCreated> : IEventConsumer<TOrderCreated>
        where TOrderCreated : OrderCreated
    {
        private readonly IUnitOfWork uow;
        private readonly IRequestBus requestBus;

        protected OrderCreatedConsumer(IUnitOfWork uow, IMessageBus requestBus)
        {
            Guard.NotNull(requestBus, nameof(requestBus));
            Guard.NotNull(uow, nameof(uow));
            this.uow = uow;
            this.requestBus = requestBus;
        }

        public abstract OrderStatus OrderStatus { get; }

        public virtual async Task Consume(ReceiveContext<TOrderCreated> messageContext)
        {
            var messageData = messageContext.Message.Data;
            var customerRequest = new GetCustomer(messageData.CustomerId);
            var customer = await this.requestBus.Request<GetCustomer, Customer>(customerRequest);

            var orderReview = new OrderReview(
                messageData.OrderId,
                customer.Name,
                messageData.OrderTotal,
                this.OrderStatus);
            this.uow.Add(orderReview);

            foreach (var eventItem in messageData.Items)
            {
                var productRequest = new GetProduct(eventItem.ProductId);
                var product = await this.requestBus.Request<GetProduct, Product>(productRequest);

                var item = new OrderItem(
                    messageData.OrderId,
                    eventItem.LineNumber,
                    product.Name,
                    eventItem.Quantity,
                    eventItem.Price);
                this.uow.Add(item);
            }

            await this.uow.Save();
        }
    }
}