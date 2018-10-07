namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class RequestConsumerAdapter<TRequest, TResponse> : IMessageHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse> 
        where TResponse : class
    {
        private readonly IRequestConsumer<TRequest, TResponse> consumer;

        public RequestConsumerAdapter(IRequestConsumer<TRequest, TResponse> consumer)
        {
            Guard.NotNull(consumer, nameof(consumer));
            this.consumer = consumer;
        }

        public Task<TResponse> Handle(ReceiveContext<TRequest> context)
        {
            Guard.NotNull(context, nameof(context));
            return this.consumer.Consume(context);
        }
    }
}