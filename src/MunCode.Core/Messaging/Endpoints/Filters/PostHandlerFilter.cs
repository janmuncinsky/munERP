namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters.PostHandling;
    using MunCode.Core.Messaging.Messages;

    public class PostHandlerFilter<TMessageContext, TMessage, TResponse> : IMessagePipelineFilter<TMessageContext, TMessage, TResponse>
        where TMessageContext : MessageContext<TMessage>
    {
        private readonly IEnumerable<IPostHandler<TMessage, TResponse>> handlers;

        public PostHandlerFilter(ICollection<IPostHandler<TMessage, TResponse>> handlers)
        {
            Guard.NotNull(handlers, nameof(handlers));
            this.handlers = handlers;
        }

        public async Task<TResponse> Handle(TMessageContext context, MessageHandlerDelegate<TMessageContext, TMessage, TResponse> nextHandler)
        {
            Guard.NotNull(nextHandler, nameof(nextHandler));
            Guard.NotNull(context, nameof(context));
            var result = await nextHandler(context).ConfigureAwait(false);
            var tasks = this.handlers.Select(h => h.Handle(context, result));
            await Task.WhenAll(tasks).ConfigureAwait(false);
            return result;
        }
    }
}