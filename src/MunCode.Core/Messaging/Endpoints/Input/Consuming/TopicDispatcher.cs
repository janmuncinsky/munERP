namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using System;
    using System.Collections.Generic;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;

    public class TopicDispatcher : ITopicDispatcher
    {
        private readonly Dictionary<Type, string> cache = new Dictionary<Type, string>();

        public TopicDispatcher(IConsumerDefinition[] definitions)
        {
            Guard.NotNull(definitions, nameof(definitions));

            foreach (var definition in definitions)
            {
                var topic = "#";
                if (Attribute.GetCustomAttribute(definition.MessageConsumerType, typeof(ConsumerOfTopicAttribute))
                        is ConsumerOfTopicAttribute topicConsumer)
                {
                    topic = topicConsumer.Topic;
                }

                this.cache.Add(definition.MessageConsumerType, topic);
            }
        }

        public bool CanConsumeTopic(Type consumerType, string topic)
        {
            if (this.cache.ContainsKey(consumerType))
            {
                return this.cache[consumerType] == topic;
            }

            throw new InvalidOperationException($"Invalid consumer type '{consumerType}'.");
        }
    }
}