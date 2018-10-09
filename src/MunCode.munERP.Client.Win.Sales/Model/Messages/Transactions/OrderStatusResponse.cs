namespace MunCode.munERP.Client.Win.Sales.Model.Messages.Transactions
{
    public class OrderStatusResponse
    {
        public OrderStatusResponse(string description)
        {
            this.Description = description;
        }

        public string Description { get; }
    }
}