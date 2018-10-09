namespace MunCode.munERP.Sales.Model.Read
{
    using MunCode.Core.Design.Domain;

    public class OrderStatus : Entity<byte>
    {
        public OrderStatus(OrderStatusEnum id, string description, string longDescription) : base((byte)id)
        {
            this.Description = description;
            this.LongDescription = longDescription;
        }

        protected OrderStatus()
        {
        }

        public string Description { get; protected set; }

        public string LongDescription { get; protected set; }
    }
}