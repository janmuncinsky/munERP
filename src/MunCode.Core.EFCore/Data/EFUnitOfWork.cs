namespace MunCode.Core.Data
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Guards;

    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public EFUnitOfWork(DbContext context)
        {
            Guard.NotNull(context, nameof(context));
            this.context = context;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            Guard.NotNull(entity, nameof(entity));
            this.context.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            Guard.NotNull(entity, nameof(entity));
            this.context.Set<TEntity>().Update(entity);
        }

        public IUpdateSyntax<TEntity> Update<TEntity>() where TEntity : class
        {
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity), true);
            var syntax = new UpdateSyntax<TEntity>(this.context, entity);
            return syntax;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Guard.NotNull(entity, nameof(entity));
            this.context.Remove(entity);
        }

        public Task Save()
        {
            return this.context.SaveChangesAsync();
        }
    }
}