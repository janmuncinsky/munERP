namespace MunCode.munERP.Sales.Domain.Maps.Read
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Data.Maps;
    using MunCode.munERP.Sales.Model.Read;

    public class OrderStatusMap : ReadModelEntityMap<OrderStatus>
    {
        public override void Configure(EntityTypeBuilder<OrderStatus> builder)
        {
            builder.HasData(new OrderStatus(OrderStatusEnum.OrderAccepted, "Order accepted", "Order was successfully accepted"));
            builder.HasData(new OrderStatus(OrderStatusEnum.OrderSuspended, "Order suspended", "Order was suspended. Add more items!"));
            builder.HasData(new OrderStatus(OrderStatusEnum.ReceivableAccepted, "Receivable accepted", "Receivable was accepted. Order is confirmed."));
            builder.HasData(new OrderStatus(OrderStatusEnum.ReceivableSuspended, "Receivable suspended", "Receivable was suspended. Not enough credit."));
            builder.HasData(new OrderStatus(OrderStatusEnum.PaymentBooked, "Payment booked", "Payment for order was booked."));
        }
    }
}