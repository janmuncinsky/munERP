namespace MunCode.mERP.Sales.Api.Consumers.Events.OrderCreated
{
    using System.Threading.Tasks;

    using MunCode.Core.Data;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Sales.Model.Messages.Events.OrderCreated;
    using MunCode.mERP.Sales.Model.Messages.Responses;
    using MunCode.mERP.Sales.Model.Read;

    public class SuspendedOrderCreatedConsumer : OrderCreatedConsumer<SuspendedOrderCreated>
    {
        public SuspendedOrderCreatedConsumer(IUnitOfWork uow, IMessageBus requestBus)
            : base(uow, requestBus)
        {
        }

        public override OrderStatus OrderStatus => OrderStatus.OrderSuspended;

        public override async Task Consume(ReceiveContext<SuspendedOrderCreated> messageContext)
        {
            await base.Consume(messageContext);
            await messageContext.Respond(new OrderStatusResponse(this.OrderStatus));
        }
    }
}