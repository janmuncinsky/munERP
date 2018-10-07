namespace MunCode.munERP.Accounting.Domain.Maps
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Design.Domain;

    public class AuditTrailMap : IEntityTypeConfiguration<AuditTrail>
    {
        public void Configure(EntityTypeBuilder<AuditTrail> builder)
        {
            builder.Property("created").HasColumnName("Created").IsRequired();
            builder.Property("createdBy").HasColumnName("CreatedBy").IsRequired();
        }
    }
}