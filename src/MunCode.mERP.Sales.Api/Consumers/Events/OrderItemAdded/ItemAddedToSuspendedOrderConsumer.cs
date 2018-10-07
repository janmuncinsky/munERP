namespace MunCode.mERP.Sales.Api.Consumers.Events.OrderItemAdded
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Messages.Events.OrderItemAdded;
    using MunCode.mERP.Sales.Model.Messages.Responses;
    using MunCode.mERP.Sales.Model.Read;

    public class ItemAddedToSuspendedOrderConsumer : OrderItemAddedConsumer<ItemAddedToSuspendedOrder>
    {
        public ItemAddedToSuspendedOrderConsumer(IUnitOfWork uow, IMessageBus requestBus)
            : base(uow, requestBus)
        {
        }

        public override OrderStatus OrderStatus => OrderStatus.OrderSuspended;

        public override async Task Consume(ReceiveContext<ItemAddedToSuspendedOrder> messageContext)
        {
            await base.Consume(messageContext);
            await messageContext.Respond(new OrderStatusResponse(this.OrderStatus));
        }
    }
}