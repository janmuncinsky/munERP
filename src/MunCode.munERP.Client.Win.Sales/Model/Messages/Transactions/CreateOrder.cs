namespace MunCode.munERP.Client.Win.Sales.Model.Messages.Transactions
{
    using System.Collections.Generic;

    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Client.Win.Sales.UI.CreateOrder;

    [BusyNotifierMessage(typeof(CreateOrderViewModel)), ClosingNotifierMessage(typeof(CreateOrderViewModel))]
    public class CreateOrder : ITransaction<OrderStatusResponse>
    {
        public CreateOrder(int customerId, IEnumerable<OrderItem> items)
        {
            this.CustomerId = customerId;
            this.Items = items;
        }

        public int CustomerId { get; }

        public IEnumerable<OrderItem> Items { get; }
    }
}