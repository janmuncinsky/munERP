namespace MunCode.mERP.Sales.Domain.Maps.Read
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Data.Maps;
    using MunCode.mERP.Sales.Model.Read;

    public class ProductMap : ReadModelEntityMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.Property(i => i.Name).IsRequired();
            builder.HasData(
                new { Id = 1, Name = "iPhone 8" },
                new { Id = 2, Name = "samsung galaxy s8" },
                new { Id = 3, Name = "Nokia 5" },
                new { Id = 4, Name = "Huawei Y6" });
            builder.OwnsOne(
                o => o.Price,
                b =>
                    {
                        b.Property("amount").HasColumnName("Amount").IsRequired();
                        b.Property("currency").HasColumnName("Currency").IsRequired();
                        b.HasData(
                            new { ProductId = 1, amount = 500M, currency = "USD" },
                            new { ProductId = 2, amount = 300M, currency = "USD" },
                            new { ProductId = 3, amount = 250M, currency = "USD" },
                            new { ProductId = 4, amount = 200M, currency = "USD" });
                    });
        }
    }
}