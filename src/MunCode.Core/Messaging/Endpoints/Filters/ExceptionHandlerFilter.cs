namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters.ExceptionHandling;
    using MunCode.Core.Messaging.Messages;

    public class ExceptionHandlerFilter<TMessageContext, TMessage, TResponse> : IMessagePipelineFilter<TMessageContext, TMessage, TResponse>
        where TMessageContext : MessageContext<TMessage>
    {
        private readonly IEnumerable<IExceptionHandler<TMessage>> handlers;

        public ExceptionHandlerFilter(ICollection<IExceptionHandler<TMessage>> handlers)
        {
            Guard.NotNull(handlers, nameof(handlers));
            this.handlers = handlers;
        }

        public async Task<TResponse> Handle(TMessageContext context, MessageHandlerDelegate<TMessageContext, TMessage, TResponse> nextHandler)
        {
            Guard.NotNull(nextHandler, nameof(nextHandler));
            Guard.NotNull(context, nameof(context));
            try
            {
                return await nextHandler(context).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var tasks = this.handlers.Select(h => h.Handle(context, e));
                await Task.WhenAll(tasks).ConfigureAwait(false);
                throw;
            }
        }
    }
}