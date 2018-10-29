namespace MunCode.munERP.Accounting.Domain.Maps.Read
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Data.Maps;
    using MunCode.munERP.Accounting.Model.Read;

    public class CustomerBalanceReviewMap : ReadModelEntityMap<CustomerBalanceReview>
    {
        public override void Configure(EntityTypeBuilder<CustomerBalanceReview> builder)
        {
            base.Configure(builder);
            builder.Property(i => i.CustomerName).IsRequired();
            builder.HasData(new { Id = 1, CustomerName = "Jan Muncinsky" }, new { Id = 2, CustomerName = "Ondrej Muncinsky" });
            builder.OwnsOne(
                c => c.CreditTotal,
                b =>
                    {
                        b.Property("amount").HasColumnName("CreditTotalAmount").IsRequired();
                        b.Property("currency").HasColumnName("CreditTotalCurrency").IsRequired();
                        b.HasData(
                            new { CustomerBalanceReviewId = 1, amount = 700M, currency = "USD" },
                            new { CustomerBalanceReviewId = 2, amount = 800M, currency = "USD" });
                    });
            builder.OwnsOne(
                c => c.ReceivableTotal,
                b =>
                    {
                        b.Property("amount").HasColumnName("ReceivableTotalAmount").IsRequired();
                        b.Property("currency").HasColumnName("ReceivableTotalCurrency").IsRequired();
                        b.HasData(
                            new { CustomerBalanceReviewId = 1, amount = 0M, currency = "USD" },
                            new { CustomerBalanceReviewId = 2, amount = 0M, currency = "USD" });
                    });
        }
    }
}