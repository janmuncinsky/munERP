namespace MunCode.mERP.Sales.Model.Messages.Events.OrderItemAdded
{
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public abstract class OrderItemAdded : IEvent
    {
        protected OrderItemAdded(OrderItemAddedData data)
        {
            Guard.NotNull(data, nameof(data));
            this.Data = data;
        }

        public OrderItemAddedData Data { get; }
    }
}