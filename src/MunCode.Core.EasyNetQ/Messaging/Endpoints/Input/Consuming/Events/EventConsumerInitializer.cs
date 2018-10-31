namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Events
{
    using System;

    using global::EasyNetQ;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class EventConsumerInitializer<TEvent, THandler> : IMessageConsumerInitializer
        where TEvent : class, IEvent
        where THandler : IEventConsumer<TEvent>
    {
        private readonly EventConsumerDefinition<TEvent, THandler> eventConsumerDefinition;
        private readonly IBus easyNetQBus;
        private readonly IEasyNetQMessageHandlerDispatcher dispatcher;
        private readonly ConnectionConfiguration connectionConfiguration;

        public EventConsumerInitializer(
            EventConsumerDefinition<TEvent, THandler> eventConsumerDefinition,
            IBus easyNetQBus,
            IEasyNetQMessageHandlerDispatcher dispatcher, 
            ConnectionConfiguration connectionConfiguration)
        {
            Guard.NotNull(connectionConfiguration, nameof(connectionConfiguration));
            Guard.NotNull(dispatcher, nameof(dispatcher));
            Guard.NotNull(easyNetQBus, nameof(easyNetQBus));
            Guard.NotNull(eventConsumerDefinition, nameof(eventConsumerDefinition));
            this.eventConsumerDefinition = eventConsumerDefinition;
            this.easyNetQBus = easyNetQBus;
            this.dispatcher = dispatcher;
            this.connectionConfiguration = connectionConfiguration;
        }

        public void Initialize()
        {
            const string DefaultTopic = "#";

            var bus = this.easyNetQBus.Advanced;
            var queueName = bus.Conventions.QueueNamingConvention(typeof(TEvent), this.eventConsumerDefinition.MessageConsumerName);
            var exchangeName = bus.Conventions.ExchangeNamingConvention(typeof(TEvent));
            var queue = this.easyNetQBus.Advanced.QueueDeclare(queueName);
            var exchange = bus.ExchangeDeclare(exchangeName, "topic");
            var topic = DefaultTopic;

            if (Attribute.GetCustomAttribute(this.eventConsumerDefinition.MessageConsumerType, typeof(ConsumerOfTopicAttribute)) 
                    is ConsumerOfTopicAttribute topicConsumer)
            {
                topic = topicConsumer.Topic;
            }

            bus.Bind(exchange, queue, topic);

            bus.Consume<TEvent>(
                queue,
                (message, messageReceivedInfo) => this.dispatcher.Dispatch(message, topic == DefaultTopic ? string.Empty : topic),
                x => { x.WithPrefetchCount(this.connectionConfiguration.PrefetchCount); });
        }
    }
}