namespace MunCode.mERP.Accounting.Domain.Maps
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Data.Maps;
    using MunCode.Core.Design.Domain;
    using MunCode.mERP.Accounting.Model.Domain;

    public class ReceivableMap : DomainModelEntityMap<Receivable>
    {
        public override void Configure(EntityTypeBuilder<Receivable> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(
                typeof(Money),
                "amount",
                b =>
                    {
                        b.Property("amount").HasColumnName("Amount").IsRequired();
                        b.Property("currency").HasColumnName("Currency").IsRequired();
                    });
            builder.OwnsOne(typeof(AuditTrail), "auditTrail");
            builder.Property("isSuspended").HasColumnName("IsSuspended");
        }
    }
}