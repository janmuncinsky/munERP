namespace MunCode.mERP.Client.Win.Sales.Model.Read
{
    using System;

    using MunCode.mERP.Client.Win.Sales.Model.Messages.Transactions;

    public class OrderReview
    {
        public OrderReview(Guid id, string customerName, Money orderTotal, OrderStatus orderStatus)
        {
            this.Id = id;
            this.CustomerName = customerName;
            this.OrderTotal = orderTotal;
            this.OrderStatus = orderStatus;
        }

        public Guid Id { get; }

        public string CustomerName { get; }

        public Money OrderTotal { get; }

        public OrderStatus OrderStatus { get; }
    }
}