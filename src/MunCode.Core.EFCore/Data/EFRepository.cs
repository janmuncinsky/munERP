namespace MunCode.Core.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters.OutBox;

    public class EFRepository<TAggregate, TId> : IRepository<TAggregate, TId>
        where TAggregate : Aggregate<TId>
    {
        private readonly DbContext context;
        private readonly IOutbox outbox;

        public EFRepository(DbContext context, IOutbox outbox)
        {
            Guard.NotNull(context, nameof(context));
            Guard.NotNull(outbox, nameof(outbox));
            this.context = context;
            this.outbox = outbox;
        }

        public virtual async Task<TAggregate> Get(TId id)
        {
            var result = await this.LoadAdditionalData(this.context.Set<TAggregate>())
                             .FirstOrDefaultAsync(a => id.Equals(a.Id));

            if (result == null)
            {
                throw new InvalidOperationException($"Can't find any '{typeof(TAggregate).Name}' with id - '{id}'");
            }

            return result;
        }

        public virtual async Task Save(TAggregate aggregate)
        {
            Guard.NotNull(aggregate, nameof(aggregate));
            if (aggregate.DomainEvents.Any(e => e is IAggregateCreatedEvent))
            {
                this.context.Set<TAggregate>().Add(aggregate);
            }

            await this.context.SaveChangesAsync();
            this.PublishEvents();
        }

        protected virtual IQueryable<TAggregate> LoadAdditionalData(DbSet<TAggregate> set)
        {
            return set;
        }

        private void PublishEvents()
        {
            var domainEntities = this.context.ChangeTracker.Entries<IHaveDomainEvents>()
                .Where(e => e.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities.SelectMany(x => x.Entity.DomainEvents).ToList();
            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());
            domainEvents.ForEach(e => this.outbox.Enqueue(e));
        }
    }
}