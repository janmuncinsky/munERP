namespace MunCode.munERP.Sales.Model.Read
{
    using System;

    using MunCode.Core.Design.Domain;

    public class OrderItem
    {
        public OrderItem(Guid orderId, short lineNumber, string productName, short quantity, Money price)
        {
            this.OrderId = orderId;
            this.LineNumber = lineNumber;
            this.ProductName = productName;
            this.Quantity = quantity;
            this.Price = price;
        }

        protected OrderItem()
        {
        }

        public int Id { get; protected set; }

        public Guid OrderId { get; protected set; }

        public short LineNumber { get; protected set; }

        public string ProductName { get; protected set; }

        public short Quantity { get; protected set; }

        public Money Price { get; protected set; }
    }
}