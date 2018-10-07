namespace MunCode.munERP.Sales.Domain
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using MunCode.Core.Data;

    public class SalesContext : BaseDbContext
    {
        public SalesContext(IOptions<DatabaseConfig> databaseConfig) : base(databaseConfig)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Maps.Domain.OrderMap());
            modelBuilder.ApplyConfiguration(new Maps.Read.OrderReviewMap());
            modelBuilder.ApplyConfiguration(new Maps.Read.OrderItemMap());
            modelBuilder.ApplyConfiguration(new Maps.Read.CustomerMap());
            modelBuilder.ApplyConfiguration(new Maps.Read.ProductMap());
        }
    }
}