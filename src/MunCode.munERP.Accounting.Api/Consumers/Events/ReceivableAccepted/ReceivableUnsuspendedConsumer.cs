namespace MunCode.munERP.Accounting.Api.Consumers.Events.ReceivableAccepted
{
    using MunCode.Core.Data;
    using MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked;

    public class ReceivableUnsuspendedConsumer : ReceivableAcceptedConsumer<ReceivableUnsuspended>
    {
        public ReceivableUnsuspendedConsumer(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}