namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class MediatorBus : IMessageBus
    {
        private readonly IOutputMessageChannel channel;
        private readonly IMessageContextFactory messageContextFactory;

        public MediatorBus(IOutputMessageChannel channel, IMessageContextFactory messageContextFactory)
        {
            Guard.NotNull(messageContextFactory, nameof(messageContextFactory));
            Guard.NotNull(channel, nameof(channel));
            this.channel = channel;
            this.messageContextFactory = messageContextFactory;
        }

        public Task Send<TCommand>(TCommand command)
            where TCommand : class, ICommand
        {
            Guard.NotNull(command, nameof(command));
            return this.channel.Send(this.CreateContext(command));
        }

        public Task Publish<TEvent>(TEvent @event)
            where TEvent : class, IEvent
        {
            Guard.NotNull(@event, nameof(@event));
            return this.channel.Publish(this.CreateContext(@event));
        }

        public Task<TResponse> Request<TRequest, TResponse>(TRequest request)
            where TRequest : class, IRequest<TResponse> where TResponse : class
        {
            Guard.NotNull(request, nameof(request));
            return this.channel.Request<TRequest, TResponse>(this.CreateContext(request));
        }

        private SendContext<TMessage> CreateContext<TMessage>(TMessage message)
        {
            Guard.NotNull(message, nameof(message));
            return this.messageContextFactory.Create<SendContext<TMessage>, TMessage>(message, new MessageMetadata());
        }
    }
}