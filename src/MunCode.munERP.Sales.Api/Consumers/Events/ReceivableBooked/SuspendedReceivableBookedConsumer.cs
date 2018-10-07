namespace MunCode.munERP.Sales.Api.Consumers.Events.ReceivableBooked
{
    using MunCode.Core.Data;
    using MunCode.munERP.Sales.Model.Messages.Events.ReceivableBooked;
    using MunCode.munERP.Sales.Model.Read;

    public class SuspendedReceivableBookedConsumer : ReceivableBookedConsumer<SuspendedReceivableBooked>
    {
        public SuspendedReceivableBookedConsumer(IUnitOfWork uow)
            : base(uow)
        {
        }

        public override OrderStatus Status => OrderStatus.ReceivableSuspended;
    }
}