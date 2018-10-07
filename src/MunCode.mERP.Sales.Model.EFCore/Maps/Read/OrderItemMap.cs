namespace MunCode.mERP.Sales.Domain.Maps.Read
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Data.Maps;
    using MunCode.mERP.Sales.Model.Read;

    public class OrderItemMap : ReadModelEntityMap<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(
                o => o.Price,
                b =>
                    {
                        b.Property("amount").HasColumnName("Amount").IsRequired();
                        b.Property("currency").HasColumnName("Currency").IsRequired();
                    });
            builder.Property(i => i.ProductName).IsRequired();
        }
    }
}