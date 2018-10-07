namespace MunCode.munERP.Sales.Api.Consumers.Events.ReceivableBooked
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Events.ReceivableBooked;
    using MunCode.munERP.Sales.Model.Messages.Responses;
    using MunCode.munERP.Sales.Model.Read;

    public abstract class ReceivableBookedConsumer<TEvent> : IEventConsumer<TEvent>
        where TEvent : ReceivableBooked
    {
        private readonly IUnitOfWork uow;

        protected ReceivableBookedConsumer(IUnitOfWork uow)
        {
            Guard.NotNull(uow, nameof(uow));
            this.uow = uow;
        }

        public abstract OrderStatus Status { get; }

        public async Task Consume(ReceiveContext<TEvent> messageContext)
        {
            var @event = messageContext.Message;
            this.uow.Update<OrderReview>()
                .SetValue(o => o.OrderStatus, this.Status)
                .Where(o => o.Id, @event.OrderId);

            await this.uow.Save();
            await messageContext.Respond(new OrderStatusResponse(this.Status));
        }
    }
}