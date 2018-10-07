namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class OutputMessageChannelDefinition<TMessageChannelConfigurator, TChannel> : IOutputMessageChannelDefinition
        where TMessageChannelConfigurator : IMessageChannelConfigurator
        where TChannel : IOutputMessageChannel
    {
        private readonly TMessageChannelConfigurator configurator;
        private readonly TChannel channel;

        public OutputMessageChannelDefinition(TMessageChannelConfigurator configurator, TChannel channel)
        {
            Guard.NotNull(channel, nameof(channel));
            Guard.NotNull(configurator, nameof(configurator));
            this.configurator = configurator;
            this.channel = channel;
        }


        public bool CanRoute(Type messageType)
        {
            Guard.NotNull(messageType, nameof(messageType));
            return this.configurator.CanRoute(messageType);
        }

        public Task Send<TCommand>(SendContext<TCommand> context)
            where TCommand : class, ICommand
        {
            Guard.NotNull(context, nameof(context));
            return this.channel.Send(context);
        }

        public Task Publish<TEvent>(SendContext<TEvent> context)
            where TEvent : class, IEvent
        {
            Guard.NotNull(context, nameof(context));
            return this.channel.Publish(context);
        }

        public Task<TResponse> Request<TRequest, TResponse>(SendContext<TRequest> context)
            where TRequest : class, IRequest<TResponse> where TResponse : class
        {
            Guard.NotNull(context, nameof(context));
            return this.channel.Request<TRequest, TResponse>(context);
        }

        public Task Respond<TResponse>(SendContext<TResponse> context)
            where TResponse : class
        {
            Guard.NotNull(context, nameof(context));
            return this.channel.Respond(context);
        }
    }
}