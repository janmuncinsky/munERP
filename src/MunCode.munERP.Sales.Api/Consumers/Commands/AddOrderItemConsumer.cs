namespace MunCode.munERP.Sales.Api.Consumers.Commands
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Domain;
    using MunCode.munERP.Sales.Model.Messages.Commands;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Read;

    using OrderItem = MunCode.munERP.Sales.Model.Messages.Events.OrderItem;
    
    public class AddOrderItemConsumer : ICommandConsumer<AddOrderItem>
    {
        private readonly IRepository<Order, Guid> repository;
        private readonly IMessageBus bus;

        public AddOrderItemConsumer(IRepository<Order, Guid> repository, IMessageBus bus)
        {
            Guard.NotNull(bus, nameof(bus));
            Guard.NotNull(repository, nameof(repository));
            this.repository = repository;
            this.bus = bus;
        }

        public async Task Consume(ReceiveContext<AddOrderItem> messageContext)
        {
            Guard.NotNull(messageContext, nameof(messageContext));
            var message = messageContext.Message;
            var order = await this.repository.Get(message.OrderId);
            var getProduct = new GetProduct(message.OrderItem.ProductId);
            var product = await this.bus.Request<GetProduct, Product>(getProduct);
            var orderItem = new OrderItem(message.OrderItem.LineNumber, product.Id, product.Price, message.OrderItem.Quantity);
            order.AddItem(orderItem);
            await this.repository.Save(order);
        }
    }
}