namespace MunCode.mERP.Sales.Api.Consumers.Events.ReceivableBooked
{
    using MunCode.Core.Data;
    using MunCode.mERP.Sales.Model.Messages.Events.ReceivableBooked;
    using MunCode.mERP.Sales.Model.Read;

    public class AcceptedReceivableBookedConsumer : ReceivableBookedConsumer<AcceptedReceivableBooked>
    {
        public AcceptedReceivableBookedConsumer(IUnitOfWork uow)
            : base(uow)
        {
        }

        public override OrderStatus Status => OrderStatus.ReceivableAccepted;
    }
}