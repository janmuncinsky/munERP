namespace MunCode.munERP.Client.Win.Sales.Model.Read
{
    using System;

    public class OrderReview
    {
        public OrderReview(Guid id, string customerName, Money orderTotal, string orderStatusDescription)
        {
            this.Id = id;
            this.CustomerName = customerName;
            this.OrderTotal = orderTotal;
            this.OrderStatusDescription = orderStatusDescription;
        }

        public Guid Id { get; }

        public string CustomerName { get; }

        public Money OrderTotal { get; }

        public string OrderStatusDescription { get; }
    }
}