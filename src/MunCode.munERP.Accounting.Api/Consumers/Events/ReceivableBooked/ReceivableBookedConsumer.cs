namespace MunCode.munERP.Accounting.Api.Consumers.Events.ReceivableBooked
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked;
    using MunCode.munERP.Accounting.Model.Read;

    public class ReceivableBookedConsumer : IEventConsumer<ReceivableBooked>
    {
        private readonly IUnitOfWork uow;

        public ReceivableBookedConsumer(IUnitOfWork uow)
        {
            Guard.NotNull(uow, nameof(uow));
            this.uow = uow;
        }

        public Task Consume(ReceiveContext<ReceivableBooked> messageContext)
        {
            var message = messageContext.Message;
            this.uow.Update<CustomerBalanceReview>()
                .SetReference(c => c.ReceivableTotal, message.ReceivableTotal)
                .Where(r => r.Id, message.CustomerBalanceId);

            return this.uow.Save();
        }
    }
}