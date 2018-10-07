namespace MunCode.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Guards;

    public class UpdateSyntax<TEntity> : IUpdateSyntax<TEntity>
        where TEntity : class
    {
        private readonly DbContext context;
        private readonly TEntity entity;
        private readonly List<Action> propertyModifiers = new List<Action>();

        public UpdateSyntax(DbContext context, TEntity entity)
        {
            this.context = context;
            this.entity = entity;
        }

        public IUpdateSyntax<TEntity> SetReference<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            TProperty value)
            where TProperty : class
        {
            void PropertyModifier()
            {
                var reference = this.context.Entry(this.entity).Reference(propertyExpression);
                reference.CurrentValue = value;
                reference.TargetEntry.State = EntityState.Modified;
            }

            this.propertyModifiers.Add(PropertyModifier);
            return this;
        }

        public IUpdateSyntax<TEntity> SetValue<TProperty>(
            Expression<Func<TEntity, TProperty>> propertyExpression,
            TProperty value)
        {
            void PropertyModifier() => this.context.Entry(this.entity).Property(propertyExpression).CurrentValue = value;
            this.propertyModifiers.Add(PropertyModifier);
            return this;
        }

        public void Where<TId>(Expression<Func<TEntity, TId>> idExpression, TId value)
        {
            Guard.NotNull(idExpression, nameof(idExpression));
            this.context.Entry(this.entity).Property(idExpression).CurrentValue = value;
            this.context.Set<TEntity>().Attach(this.entity);
            foreach (var propertyModification in this.propertyModifiers)
            {
                propertyModification();
            }
        }
    }
}