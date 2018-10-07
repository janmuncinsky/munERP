namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands
{
    using System.Collections.Concurrent;

    using global::EasyNetQ;
    using global::EasyNetQ.Topology;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input;
    using MunCode.Core.Messaging.Messages;

    public class CommandConsumerInitializer<TCommand, THandler> : IMessageConsumerInitializer
        where TCommand : class, ICommand where THandler : ICommandConsumer<TCommand>
    {
        private readonly CommandConsumerDefinition<TCommand, THandler> commandConsumerDefinition;
        private readonly ConcurrentDictionary<string, IQueue> declaredQueues = new ConcurrentDictionary<string, IQueue>();
        private readonly IBus easyNetQBus;
        private readonly IEasyNetQMessageHandlerDispatcher dispatcher;

        public CommandConsumerInitializer(
            CommandConsumerDefinition<TCommand, THandler> commandConsumerDefinition,
            IBus easyNetQBus,
            IEasyNetQMessageHandlerDispatcher dispatcher)
        {
            Guard.NotNull(dispatcher, nameof(dispatcher));
            Guard.NotNull(easyNetQBus, nameof(easyNetQBus));
            Guard.NotNull(commandConsumerDefinition, nameof(commandConsumerDefinition));

            this.commandConsumerDefinition = commandConsumerDefinition;
            this.easyNetQBus = easyNetQBus;
            this.dispatcher = dispatcher;
        }

        public void Initialize()
        {
            var queue = this.DeclareQueue(this.commandConsumerDefinition.MessageName);
            this.easyNetQBus.Advanced.Consume(queue, (IMessage<TCommand> message, MessageReceivedInfo info) => this.dispatcher.Dispatch(message));
        }

        private IQueue DeclareQueue(string queueName)
        {
            IQueue queue = null;
            this.declaredQueues.GetOrAdd(queueName, key => queue = this.easyNetQBus.Advanced.QueueDeclare(queueName));
            return queue;
        }
    }
}