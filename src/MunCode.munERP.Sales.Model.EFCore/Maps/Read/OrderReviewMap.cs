namespace MunCode.munERP.Sales.Domain.Maps.Read
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Data.Maps;
    using MunCode.munERP.Sales.Model.Read;

    public class OrderReviewMap : ReadModelEntityMap<OrderReview>
    {
        public override void Configure(EntityTypeBuilder<OrderReview> builder)
        {
            base.Configure(builder);
            builder.Property(i => i.CustomerName).IsRequired();
            builder.OwnsOne(
                o => o.OrderTotal,
                b =>
                    {
                        b.Property("amount").HasColumnName("Amount").IsRequired();
                        b.Property("currency").HasColumnName("Currency").IsRequired();
                    });
        }
    }
}