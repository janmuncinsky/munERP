namespace MunCode.munERP.Accounting.Domain
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Data;
    using MunCode.Core.Messaging.Endpoints.Filters.OutBox;
    using MunCode.munERP.Accounting.Model.Domain;

    public class CustomerBalanceRepository : EFRepository<CustomerBalance, int>
    {
        public CustomerBalanceRepository(DbContext context, IOutbox outbox)
            : base(context, outbox)
        {
        }

        protected override IQueryable<CustomerBalance> LoadAdditionalData(DbSet<CustomerBalance> set)
        {
            return base.LoadAdditionalData(set).Include("receivables");
        }
    }
}