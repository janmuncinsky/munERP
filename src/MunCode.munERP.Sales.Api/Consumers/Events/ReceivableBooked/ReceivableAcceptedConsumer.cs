namespace MunCode.munERP.Sales.Api.Consumers.Events.ReceivableBooked
{
    using MunCode.Core.Data;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.munERP.Sales.Model.Messages.Events.ReceivableBooked;
    using MunCode.munERP.Sales.Model.Read;

    public class ReceivableAcceptedConsumer : ReceivableBookedConsumer<ReceivableAccepted>
    {
        public ReceivableAcceptedConsumer(IUnitOfWork uow, IRequestBus requestBus)
            : base(uow, requestBus)
        {
        }

        public override OrderStatusEnum OrderStatus => OrderStatusEnum.ReceivableAccepted;
    }
}