namespace MunCode.Core.Data
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        void Add<TEntity>(TEntity entity) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;

        IUpdateSyntax<TEntity> Update<TEntity>() where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        Task Save();
    }
}