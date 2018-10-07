namespace MunCode.munERP.Sales.Domain.Maps.Domain
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Data.Maps;
    using MunCode.Core.Design.Domain;
    using MunCode.munERP.Sales.Model.Domain;

    public class OrderMap : DomainModelEntityMap<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(
                typeof(Money),
                "orderTotal",
                b =>
                    {
                        b.Property("amount").HasColumnName("OrderTotalAmount").IsRequired();
                        b.Property("currency").HasColumnName("OrderTotalCurrency").IsRequired();
                    });

            builder.Property("customerId");
            builder.Property("isSuspended").HasColumnName("IsSuspended");
        }
    }
}