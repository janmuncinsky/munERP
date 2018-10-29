namespace MunCode.munERP.Accounting.Api.Consumers.Events.ReceivableAccepted
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Accounting.Model.Messages.Events.ReceivableBooked;
    using MunCode.munERP.Accounting.Model.Read;

    public abstract class ReceivableAcceptedConsumer<TEvent> : IEventConsumer<TEvent>
        where TEvent : ReceivableAccepted
    {
        private readonly IUnitOfWork uow;

        protected ReceivableAcceptedConsumer(IUnitOfWork uow)
        {
            Guard.NotNull(uow, nameof(uow));
            this.uow = uow;
        }

        public Task Consume(ReceiveContext<TEvent> messageContext)
        {
            var message = messageContext.Message;
            this.uow.Update<CustomerBalanceReview>()
                .SetReference(c => c.ReceivableTotal, message.Amount)
                .Where(r => r.Id, message.CustomerBalanceId);

            return this.uow.Save();
        }
    }
}