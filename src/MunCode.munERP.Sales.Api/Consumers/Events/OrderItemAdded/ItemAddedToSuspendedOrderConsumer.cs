namespace MunCode.munERP.Sales.Api.Consumers.Events.OrderItemAdded
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Events.OrderItemAdded;
    using MunCode.munERP.Sales.Model.Messages.Responses;
    using MunCode.munERP.Sales.Model.Read;

    public class ItemAddedToSuspendedOrderConsumer : OrderItemAddedConsumer<ItemAddedToSuspendedOrder>
    {
        public ItemAddedToSuspendedOrderConsumer(IUnitOfWork uow, IMessageBus requestBus)
            : base(uow, requestBus)
        {
        }

        public override OrderStatusEnum OrderStatus => OrderStatusEnum.OrderSuspended;

        public override async Task Consume(ReceiveContext<ItemAddedToSuspendedOrder> messageContext)
        {
            await base.Consume(messageContext);
            var orderStatus = await this.GetOrderStatus();
            var response = new OrderStatusResponse(orderStatus.LongDescription);
            await messageContext.Respond(response);
        }
    }
}