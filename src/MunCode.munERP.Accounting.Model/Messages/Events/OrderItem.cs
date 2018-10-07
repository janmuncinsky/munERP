namespace MunCode.munERP.Accounting.Model.Messages.Events
{
    using MunCode.Core.Design.Domain;

    public class OrderItem
    {
        public OrderItem(short lineNumber, int productId, Money price, short quantity)
        {
            this.LineNumber = lineNumber;
            this.ProductId = productId;
            this.Price = price;
            this.Quantity = quantity;
        }

        public short LineNumber { get; }

        public Money Price { get; }

        public short Quantity { get; }

        public int ProductId { get; }

    }
}