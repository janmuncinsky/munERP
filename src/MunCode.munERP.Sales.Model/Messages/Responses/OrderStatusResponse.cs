namespace MunCode.munERP.Sales.Model.Messages.Responses
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