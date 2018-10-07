namespace MunCode.munERP.Sales.Model.Read
{
    using System;

    using MunCode.Core.Design.Domain;

    public class OrderReview
    {
        public OrderReview(Guid id, string customerName, Money orderTotal, OrderStatus orderStatus)
        {
            this.Id = id;
            this.CustomerName = customerName;
            this.OrderTotal = orderTotal;
            this.OrderStatus = orderStatus;
        }

        protected OrderReview()
        {
        }

        public Guid Id { get; protected set; }

        public string CustomerName { get; protected set; }

        public Money OrderTotal { get; protected set; }

        public OrderStatus OrderStatus { get; protected set; }
    }
}