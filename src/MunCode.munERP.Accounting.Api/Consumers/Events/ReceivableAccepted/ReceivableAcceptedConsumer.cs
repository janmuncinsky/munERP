namespace MunCode.munERP.Accounting.Api.Consumers.Events.ReceivableAccepted
{
    using MunCode.Core.Data;
    using MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked;

    public class ReceivableAcceptedConsumer : ReceivableAcceptedConsumer<ReceivableAccepted>
    {
        public ReceivableAcceptedConsumer(IUnitOfWork uow)
            : base(uow)
        {
        }
    }
}