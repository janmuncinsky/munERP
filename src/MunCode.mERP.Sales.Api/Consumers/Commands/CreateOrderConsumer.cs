namespace MunCode.mERP.Sales.Api.Consumers.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Domain;
    using MunCode.mERP.Sales.Model.Messages.Commands;
    using MunCode.mERP.Sales.Model.Messages.Requests;
    using MunCode.mERP.Sales.Model.Read;

    using OrderItem = MunCode.mERP.Sales.Model.Messages.Events.OrderItem;

    public class CreateOrderConsumer : ICommandConsumer<CreateOrder>
    {
        private readonly IRepository<Order, Guid> repository;
        private readonly IMessageBus messageBus;

        public CreateOrderConsumer(IRepository<Order, Guid> repository, IMessageBus messageBus)
        {
            Guard.NotNull(messageBus, nameof(messageBus));
            Guard.NotNull(repository, nameof(repository));
            this.repository = repository;
            this.messageBus = messageBus;
        }

        public async Task Consume(ReceiveContext<CreateOrder> messageContext)
        {
            Guard.NotNull(messageContext, nameof(messageContext));
            var items = new List<OrderItem>();

            foreach (var messageItem in messageContext.Message.Items)
            {
                var getProduct = new GetProduct(messageItem.ProductId);
                var product = await this.messageBus.Request<GetProduct, Product>(getProduct);
                var orderItem = new OrderItem(messageItem.LineNumber, product.Id, product.Price, messageItem.Quantity);
                items.Add(orderItem);
            }

            var order = Order.CreateOrder(messageContext.Message.CustomerId, items);
            await this.repository.Save(order);
        }
    }
}