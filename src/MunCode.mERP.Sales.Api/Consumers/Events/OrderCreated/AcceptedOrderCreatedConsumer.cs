namespace MunCode.mERP.Sales.Api.Consumers.Events.OrderCreated
{
    using MunCode.Core.Data;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.mERP.Sales.Model.Messages.Events.OrderCreated;
    using MunCode.mERP.Sales.Model.Read;

    public class AcceptedOrderCreatedConsumer : OrderCreatedConsumer<AcceptedOrderCreated>
    {
        public AcceptedOrderCreatedConsumer(IUnitOfWork uow, IMessageBus requestBus)
            : base(uow, requestBus)
        {
        }

        public override OrderStatus OrderStatus => OrderStatus.OrderAccepted;
    }
}