namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Messages;

    public class MessageRouter : IOutputMessageChannel
    {
        private readonly ConcurrentDictionary<Type, IOutputMessageChannel> channelMapping = new ConcurrentDictionary<Type, IOutputMessageChannel>();
        private readonly ICollection<IOutputMessageChannelDefinition> channels;
        private readonly IMessagePipeLine messagePipeLine;

        public MessageRouter(IMessagePipeLine messagePipeLine, ICollection<IOutputMessageChannelDefinition> channels)
        {
            Guard.NotNull(channels, nameof(channels));
            Guard.NotNull(messagePipeLine, nameof(messagePipeLine));
            this.messagePipeLine = messagePipeLine;
            this.channels = channels;
        }

        public Task Publish<TEvent>(SendContext<TEvent> context)
            where TEvent : class, IEvent
        {
            MessageHandlerDelegate<SendContext<TEvent>, TEvent, EmptyResponse> endDelegate = async c =>
                {
                    await this.GetChannel<TEvent>().Publish(c);
                    return EmptyResponse.Instance;
                };

            return this.DispatchInternal(context, endDelegate);
        }

        public Task<TResponse> Request<TRequest, TResponse>(SendContext<TRequest> context)
            where TRequest : class, IRequest<TResponse> where TResponse : class
        {
            MessageHandlerDelegate<SendContext<TRequest>, TRequest, TResponse> endDelegate = c =>
                this.GetChannel<TRequest>().Request<TRequest, TResponse>(c);

            return this.DispatchInternal(context, endDelegate);
        }

        public Task Respond<TResponse>(SendContext<TResponse> context)
            where TResponse : class
        {
            MessageHandlerDelegate<SendContext<TResponse>, TResponse, EmptyResponse> endDelegate = async c =>
                {
                    await this.GetChannel<TResponse>().Respond(c);
                    return EmptyResponse.Instance;
                };

            return this.DispatchInternal(context, endDelegate);
        }

        public Task Send<TCommand>(SendContext<TCommand> context)
            where TCommand : class, ICommand
        {
            MessageHandlerDelegate<SendContext<TCommand>, TCommand, EmptyResponse> endDelegate = async c =>
                {
                    await this.GetChannel<TCommand>().Send(c);
                    return EmptyResponse.Instance;
                };

            return this.DispatchInternal(context, endDelegate);
        }

        private Task<TResponse> DispatchInternal<TContext, TMessage, TResponse>(
            TContext context,
            MessageHandlerDelegate<TContext, TMessage, TResponse> endDelegate)
            where TContext : MessageContext<TMessage>
        {
            Guard.NotNull(context, nameof(context));
            return this.messagePipeLine.Dispatch(context, endDelegate);
        }

        private IOutputMessageChannel GetChannel<TMessage>()
        {
            var messageType = typeof(TMessage);
            return this.channelMapping.GetOrAdd(
                messageType, 
                t => this.channels.FirstOrDefault(ch => ch.CanRoute(t)) ?? 
                     throw new InvalidOperationException($"Message of type '{messageType}' is not mapped to any channel."));
        }
    }
}