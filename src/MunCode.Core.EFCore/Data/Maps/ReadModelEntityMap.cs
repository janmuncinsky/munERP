namespace MunCode.Core.Data.Maps
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ReadModelEntityMap<TEntity> : EntityMap<TEntity>
        where TEntity : class
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(typeof(TEntity).Name, "read");
        }
    }
}