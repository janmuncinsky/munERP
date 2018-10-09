namespace MunCode.munERP.Sales.Api.Consumers.Events.OrderCreated
{
    using MunCode.Core.Data;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.munERP.Sales.Model.Messages.Events.OrderCreated;
    using MunCode.munERP.Sales.Model.Read;

    public class AcceptedOrderCreatedConsumer : OrderCreatedConsumer<AcceptedOrderCreated>
    {
        public AcceptedOrderCreatedConsumer(IUnitOfWork uow, IMessageBus requestBus)
            : base(uow, requestBus)
        {
        }

        public override OrderStatusEnum OrderStatus => OrderStatusEnum.OrderAccepted;
    }
}