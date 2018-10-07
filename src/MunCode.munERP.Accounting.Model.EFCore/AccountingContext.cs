namespace MunCode.munERP.Accounting.Domain
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using MunCode.Core.Data;
    using MunCode.munERP.Accounting.Domain.Maps;

    public class AccountingContext : BaseDbContext
    {
        public AccountingContext(IOptions<DatabaseConfig> databaseConfig) : base(databaseConfig)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuditTrailMap());
            modelBuilder.ApplyConfiguration(new CustomerBalanceMap());
            modelBuilder.ApplyConfiguration(new ReceivableMap());
        }
    }
}