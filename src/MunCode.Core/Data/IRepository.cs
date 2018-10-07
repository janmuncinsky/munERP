namespace MunCode.Core.Data
{
    using System.Threading.Tasks;

    using MunCode.Core.Design.Domain;

    public interface IRepository<TAggregate, in TId>
        where TAggregate : Aggregate<TId>
    {
        Task<TAggregate> Get(TId id);

        Task Save(TAggregate aggregate);
    }
}