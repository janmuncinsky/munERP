namespace MunCode.munERP.Accounting.Model.Domain
{
    using System;

    using MunCode.Core.Design.Domain;
    using MunCode.munERP.Accounting.Model.Read;

    public class ReceivableBookingResult
    {
        public ReceivableBookingResult(Guid orderId, Money coverage, OrderStatusEnum orderStatus)
        {
            this.OrderId = orderId;
            this.Coverage = coverage;
            this.OrderStatus = orderStatus;
        }

        private ReceivableBookingResult()
        {
        }

        public static ReceivableBookingResult EmptyResult { get; } = new ReceivableBookingResult();

        public Money Coverage { get; }

        public Guid OrderId { get; }

        public OrderStatusEnum OrderStatus { get; }
    }
}