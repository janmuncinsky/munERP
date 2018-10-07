namespace MunCode.Core.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    using MunCode.Core.Guards;

    public class BaseDbContext : DbContext
    {
        private readonly string connectionString;

        public BaseDbContext(IOptions<DatabaseConfig> databaseConfig)
        {
            Guard.NotNull(databaseConfig, nameof(databaseConfig));
            this.connectionString = databaseConfig.Value.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);
        }
    }
}