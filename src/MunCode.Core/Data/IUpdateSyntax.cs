namespace MunCode.Core.Data
{
    using System;
    using System.Linq.Expressions;

    public interface IUpdateSyntax<TEntity> where TEntity : class
    {
        IUpdateSyntax<TEntity> SetValue<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value);

        IUpdateSyntax<TEntity> SetReference<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, TProperty value) 
            where TProperty : class;

        void Where<TId>(Expression<Func<TEntity, TId>> idExpression, TId value);
    }
}