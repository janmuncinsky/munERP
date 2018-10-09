namespace MunCode.munERP.Sales.Api.Consumers.Events.ReceivableBooked
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Events.ReceivableBooked;
    using MunCode.munERP.Sales.Model.Messages.Requests;
    using MunCode.munERP.Sales.Model.Messages.Responses;
    using MunCode.munERP.Sales.Model.Read;

    public abstract class ReceivableBookedConsumer<TEvent> : IEventConsumer<TEvent>
        where TEvent : ReceivableBooked
    {
        private readonly IUnitOfWork uow;
        private readonly IRequestBus requestBus;

        protected ReceivableBookedConsumer(IUnitOfWork uow, IRequestBus requestBus)
        {
            Guard.NotNull(requestBus, nameof(requestBus));
            Guard.NotNull(uow, nameof(uow));
            this.uow = uow;
            this.requestBus = requestBus;
        }

        public abstract OrderStatusEnum OrderStatus { get; }

        public async Task Consume(ReceiveContext<TEvent> messageContext)
        {
            var @event = messageContext.Message;
            var orderStatusRequest = new GetOrderStatus(this.OrderStatus);
            var orderStatus = await this.requestBus.Request<GetOrderStatus, OrderStatus>(orderStatusRequest);
            this.uow.Update<OrderReview>()
                .SetValue(o => o.OrderStatusDescription, orderStatus.Description)
                .Where(o => o.Id, @event.OrderId);

            await this.uow.Save();
            var response = new OrderStatusResponse(orderStatus.LongDescription);
            await messageContext.Respond(response);
        }
    }
}