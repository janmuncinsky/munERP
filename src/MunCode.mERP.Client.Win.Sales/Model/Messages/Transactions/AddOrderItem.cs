namespace MunCode.mERP.Client.Win.Sales.Model.Messages.Transactions
{
    using System;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Messages;
    using MunCode.mERP.Client.Win.Sales.UI.OrderReview;

    [BusyNotifierMessage(typeof(OrderReviewViewModel)), ClosingNotifierMessage(typeof(OrderReviewViewModel))]
    public class AddOrderItem : ITransaction<OrderStatusResponse>
    {
        public AddOrderItem(Guid orderId, OrderItem orderItem)
        {
            Guard.NotNull(orderItem, nameof(orderItem));
            this.OrderId = orderId;
            this.OrderItem = orderItem;
        }

        public Guid OrderId { get; }

        public OrderItem OrderItem { get; }
    }
}