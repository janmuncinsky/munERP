namespace MunCode.munERP.Sales.Model.Messages.Commands
{
    using System.Collections.Generic;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;
    using MunCode.munERP.Sales.Model.Messages.Responses;

    public class CreateOrder : ITransaction<OrderStatusResponse>
    {
        public CreateOrder(int customerId, ICollection<OrderItem> items)
        {
            Guard.NotNull(items, nameof(items));
            this.CustomerId = customerId;
            this.Items = items;
        }

        public int CustomerId { get; }

        public ICollection<OrderItem> Items { get; }
    }
}