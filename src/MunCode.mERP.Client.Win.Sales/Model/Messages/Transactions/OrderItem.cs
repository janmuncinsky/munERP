namespace MunCode.mERP.Client.Win.Sales.Model.Messages.Transactions
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