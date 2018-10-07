namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters.PreHandling;
    using MunCode.Core.Messaging.Messages;

    public class PreHandlerFilter<TMessageContext, TMessage, TResponse> : IMessagePipelineFilter<TMessageContext, TMessage, TResponse>
        where TMessageContext : MessageContext<TMessage>
    {
        private readonly IEnumerable<IPreHandler<TMessage>> handlers;

        public PreHandlerFilter(ICollection<IPreHandler<TMessage>> handlers)
        {
            Guard.NotNull(handlers, nameof(handlers));
            this.handlers = handlers;
        }

        public async Task<TResponse> Handle(TMessageContext context, MessageHandlerDelegate<TMessageContext, TMessage, TResponse> nextHandler)
        {
            Guard.NotNull(nextHandler, nameof(nextHandler));
            Guard.NotNull(context, nameof(context));
            var tasks = this.handlers.Select(h => h.Handle(context));
            await Task.WhenAll(tasks).ConfigureAwait(false);
            return await nextHandler(context).ConfigureAwait(false);
        }
    }
}