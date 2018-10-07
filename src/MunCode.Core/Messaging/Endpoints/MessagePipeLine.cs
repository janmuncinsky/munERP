namespace MunCode.Core.Messaging.Endpoints
{
    using System.Linq;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Messages;

    public class MessagePipeLine : IMessagePipeLine
    {
        private readonly ICallScopeFactory callScopeFactory;
        private readonly IMessageHandlerFiltersFactory messageHandlerFactory;

        public MessagePipeLine(
            IMessageHandlerFiltersFactory messageHandlerFactory,
            ICallScopeFactory callScopeFactory)
        {
            Guard.NotNull(callScopeFactory, nameof(callScopeFactory));
            Guard.NotNull(messageHandlerFactory, nameof(messageHandlerFactory));
            this.messageHandlerFactory = messageHandlerFactory;
            this.callScopeFactory = callScopeFactory;
        }

        public async Task<TResponse> Dispatch<TMessageContext, TMessage, TResponse>(TMessageContext context, MessageHandlerDelegate<TMessageContext, TMessage, TResponse> endDelegate)
            where TMessageContext : MessageContext<TMessage>
        {
            Guard.NotNull(endDelegate, nameof(endDelegate));
            Guard.NotNull(context, nameof(context));
            using (this.callScopeFactory.CreateScope())
            {
                var filters = this.messageHandlerFactory.Create<TMessageContext, TMessage, TResponse>();
                return await filters.Reverse().Aggregate(
                           endDelegate,
                           (nextHandler, filter) => ctx => filter.Handle(ctx, nextHandler))(context);
            }
        }
    }
}