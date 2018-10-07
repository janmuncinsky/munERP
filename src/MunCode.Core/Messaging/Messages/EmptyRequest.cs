namespace MunCode.Core.Messaging.Messages
{
    public abstract class EmptyRequest<TResponse> : Request<EmptyRequest<TResponse>, TResponse> 
    {
        public override bool Equals(EmptyRequest<TResponse> other)
        {
            return true;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
