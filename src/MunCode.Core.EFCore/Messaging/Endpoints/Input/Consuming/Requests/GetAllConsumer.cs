namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class GetAllConsumer<TRequest, TResponse> : IRequestConsumer<TRequest, TResponse[]>
        where TRequest : IRequest<TResponse[]>
        where TResponse : class
    {
        private readonly DbContext context;

        public GetAllConsumer(DbContext context)
        {
            Guard.NotNull(context, nameof(context));
            this.context = context;
        }

        public Task<TResponse[]> Consume(ReceiveContext<TRequest> messageContext)
        {
            return this.context.Set<TResponse>().ToArrayAsync();
        }
    }
}