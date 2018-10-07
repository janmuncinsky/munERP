namespace MunCode.mERP.Accounting.Domain.Maps
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Data.Maps;
    using MunCode.Core.Design.Domain;
    using MunCode.mERP.Accounting.Model.Domain;

    public class CustomerBalanceMap : DomainModelEntityMap<CustomerBalance>
    {
        public override void Configure(EntityTypeBuilder<CustomerBalance> builder)
        {
            base.Configure(builder);
            builder.HasData(new { Id = 1 }, new { Id = 2 });
            builder.OwnsOne(
                typeof(Money),
                "creditTotal",
                b =>
                    {
                        b.Property("amount").HasColumnName("CreditTotalAmount").IsRequired();
                        b.Property("currency").HasColumnName("CreditTotalCurrency").IsRequired();
                        b.HasData(
                            new { CustomerBalanceId = 1, amount = 700M, currency = "USD" },
                            new { CustomerBalanceId = 2, amount = 800M, currency = "USD" });
                    });
            builder.OwnsOne(
                typeof(Money),
                "receivableTotal",
                b =>
                    {
                        b.Property("amount").HasColumnName("ReceivableTotalAmount").IsRequired();
                        b.Property("currency").HasColumnName("ReceivableTotalCurrency").IsRequired();
                        b.HasData(
                            new { CustomerBalanceId = 1, amount = 0M, currency = "USD" },
                            new { CustomerBalanceId = 2, amount = 0M, currency = "USD" });
                    });
            builder.HasMany(typeof(Receivable), "receivables").WithOne().IsRequired().HasForeignKey("CustomerBalanceId");
        }
    }
}