namespace MunCode.munERP.Client.Win.Sales.UI.CreateOrder
{
    using MunCode.munERP.Client.Win.Sales.Model.Read;

    public class OrderItemViewModel
    {
        public OrderItemViewModel(short lineNumber, short quantity, Product product)
        {
            this.LineNumber = lineNumber;
            this.Quantity = quantity;
            this.Product = product;
        }

        public short LineNumber { get; }

        public Product Product { get; }

        public short Quantity { get; }
    }
}