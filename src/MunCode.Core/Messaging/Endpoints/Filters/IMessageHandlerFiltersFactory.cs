namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System.Collections.Generic;

    using MunCode.Core.Messaging.Messages;

    public interface IMessageHandlerFiltersFactory
    {
        IEnumerable<IMessagePipelineFilter<TMessageContext, TMessage, TResponse>> Create<TMessageContext, TMessage, TResponse>()
            where TMessageContext : MessageContext<TMessage>;
    }
}