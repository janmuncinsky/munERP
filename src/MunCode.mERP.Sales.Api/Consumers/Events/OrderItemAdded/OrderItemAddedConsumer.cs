namespace MunCode.mERP.Sales.Api.Consumers.Events.OrderItemAdded
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Messages.Events.OrderItemAdded;
    using MunCode.mERP.Sales.Model.Messages.Requests;
    using MunCode.mERP.Sales.Model.Read;

    using OrderItem = MunCode.mERP.Sales.Model.Read.OrderItem;

    public abstract class OrderItemAddedConsumer<TOrderItemAdded> : IEventConsumer<TOrderItemAdded>
        where TOrderItemAdded : OrderItemAdded
    {
        private readonly IUnitOfWork uow;
        private readonly IRequestBus requestBus;

        protected OrderItemAddedConsumer(IUnitOfWork uow, IMessageBus requestBus)
        {
            Guard.NotNull(requestBus, nameof(requestBus));
            Guard.NotNull(uow, nameof(uow));
            this.uow = uow;
            this.requestBus = requestBus;
        }

        public abstract OrderStatus OrderStatus { get; }

        public virtual async Task Consume(ReceiveContext<TOrderItemAdded> messageContext)
        {
            var messageData = messageContext.Message.Data;
            this.uow.Update<OrderReview>()
                .SetReference(o => o.OrderTotal, messageData.OrderTotal)
                .SetValue(o => o.OrderStatus, this.OrderStatus)
                .Where(o => o.Id, messageData.OrderId);

            var request = new GetProduct(messageData.OrderItem.ProductId);
            var product = await this.requestBus.Request<GetProduct, Product>(request);

            var item = new OrderItem(
                messageData.OrderId,
                messageData.OrderItem.LineNumber,
                product.Name,
                messageData.OrderItem.Quantity,
                messageData.OrderItem.Price);

            this.uow.Add(item);

            await this.uow.Save();
        }
    }
}