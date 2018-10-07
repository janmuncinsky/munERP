namespace MunCode.Core.Design.Domain
{
    public abstract class Aggregate<T> : Entity<T>
    {
        protected Aggregate()
        {
        }

        protected Aggregate(T id)
            : base(id)
        {
        }
    }
}