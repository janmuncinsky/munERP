namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests
{
    using EasyNetQ;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input;
    using MunCode.Core.Messaging.Messages;

    public class RequestConsumerInitializer<TRequest, TResponse> : IMessageConsumerInitializer
        where TRequest : class, IRequest<TResponse>
        where TResponse : class
    {
        private readonly IBus easyNetQBus;
        private readonly IInputMessageChannel dispatcher;
        private readonly IMessageContextFactory messageContextFactory;

        public RequestConsumerInitializer(IBus easyNetQBus, IInputMessageChannel dispatcher, IMessageContextFactory messageContextFactory)
        {
            Guard.NotNull(messageContextFactory, nameof(messageContextFactory));
            Guard.NotNull(dispatcher, nameof(dispatcher));
            Guard.NotNull(easyNetQBus, nameof(easyNetQBus));
            Guard.NotNull(easyNetQBus, nameof(easyNetQBus));
            this.easyNetQBus = easyNetQBus;
            this.dispatcher = dispatcher;
            this.messageContextFactory = messageContextFactory;
        }

        public void Initialize()
        {
            this.easyNetQBus.RespondAsync((TRequest r) =>
                {
                    var context = this.messageContextFactory.Create<ReceiveContext<TRequest>, TRequest>(r, new MessageMetadata());
                    return this.dispatcher.Dispatch<TRequest, TResponse>(context);
                });
        }
    }
}