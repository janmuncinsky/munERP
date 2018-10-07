namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using System;

    using MunCode.Core.Guards;

    public class ConsumerContract
    {
        public ConsumerContract(
            Type consumerDefinition,
            Type consumerInterface,
            Type consumer,
            Type messageHandlerInterface,
            Type adapter)
        {
            Guard.NotNull(adapter, nameof(adapter));
            Guard.NotNull(messageHandlerInterface, nameof(messageHandlerInterface));
            Guard.NotNull(consumer, nameof(consumer));
            Guard.NotNull(consumerInterface, nameof(consumerInterface));
            Guard.NotNull(consumerDefinition, nameof(consumerDefinition));
            this.ConsumerDefinition = consumerDefinition;
            this.ConsumerInterface = consumerInterface;
            this.Consumer = consumer;
            this.MessageHandlerInterface = messageHandlerInterface;
            this.Adapter = adapter;
        }

        public Type ConsumerDefinition { get; }

        public Type ConsumerInterface { get; }

        public Type Consumer { get; }

        public Type MessageHandlerInterface { get; }

        public Type Adapter { get; }
    }
}