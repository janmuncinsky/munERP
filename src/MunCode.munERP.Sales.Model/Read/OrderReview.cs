namespace MunCode.munERP.Sales.Model.Read
{
    using System;

    using MunCode.Core.Design.Domain;

    public class OrderReview
    {
        public OrderReview(Guid id, string customerName, Money orderTotal, string orderStatusDescription)
        {
            this.Id = id;
            this.CustomerName = customerName;
            this.OrderTotal = orderTotal;
            this.OrderStatusDescription = orderStatusDescription;
        }

        protected OrderReview()
        {
        }

        public Guid Id { get; protected set; }

        public string CustomerName { get; protected set; }

        public Money OrderTotal { get; protected set; }

        public string OrderStatusDescription { get; protected set; }
    }
}