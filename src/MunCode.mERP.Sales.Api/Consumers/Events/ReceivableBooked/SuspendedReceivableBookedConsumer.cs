namespace MunCode.mERP.Sales.Api.Consumers.Events.ReceivableBooked
{
    using MunCode.Core.Data;
    using MunCode.mERP.Sales.Model.Messages.Events.ReceivableBooked;
    using MunCode.mERP.Sales.Model.Read;

    public class SuspendedReceivableBookedConsumer : ReceivableBookedConsumer<SuspendedReceivableBooked>
    {
        public SuspendedReceivableBookedConsumer(IUnitOfWork uow)
            : base(uow)
        {
        }

        public override OrderStatus Status => OrderStatus.ReceivableSuspended;
    }
}