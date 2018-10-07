namespace MunCode.munERP.Sales.Api.Consumers.Events.OrderItemAdded
{
    using MunCode.Core.Data;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.munERP.Sales.Model.Messages.Events.OrderItemAdded;
    using MunCode.munERP.Sales.Model.Read;

    public class ItemAddedToAcceptedOrderConsumer : OrderItemAddedConsumer<ItemAddedToAcceptedOrder>
    {
        public ItemAddedToAcceptedOrderConsumer(IUnitOfWork uow, IMessageBus requestBus)
            : base(uow, requestBus)
        {
        }

        public override OrderStatus OrderStatus => OrderStatus.OrderAccepted;
    }
}