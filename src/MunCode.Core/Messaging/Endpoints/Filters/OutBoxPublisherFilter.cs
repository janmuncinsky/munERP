namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System.Linq;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters.OutBox;
    using MunCode.Core.Messaging.Messages;

    public class OutBoxPublisherFilter<TMessage, TResponse> : IMessagePipelineFilter<ReceiveContext<TMessage>, TMessage, TResponse>
    {
        private readonly IOutbox outbox;

        public OutBoxPublisherFilter(IOutbox outbox)
        {
            Guard.NotNull(outbox, nameof(outbox));
            this.outbox = outbox;
        }

        public async Task<TResponse> Handle(ReceiveContext<TMessage> context, MessageHandlerDelegate<ReceiveContext<TMessage>, TMessage, TResponse> nextHandler)
        {
            Guard.NotNull(nextHandler, nameof(nextHandler));
            Guard.NotNull(context, nameof(context));
            var response = await nextHandler(context);
            var tasks = this.outbox.Dequeue().Select(e => context.Publish((dynamic)e))
                .Cast<Task>()
                .ToList();
            await Task.WhenAll(tasks);
            return response;
        }
    }
}