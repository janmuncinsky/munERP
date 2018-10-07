namespace MunCode.munERP.Sales.Model.Messages.Commands
{
    public class OrderItem
    {
        public OrderItem(short lineNumber, int productId, short quantity)
        {
            this.LineNumber = lineNumber;
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        public short LineNumber { get; }

        public int ProductId { get; }

        public short Quantity { get; }
    }
}