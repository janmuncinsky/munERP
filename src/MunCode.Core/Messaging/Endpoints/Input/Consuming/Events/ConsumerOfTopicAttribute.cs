namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Events
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class ConsumerOfTopicAttribute : Attribute
    {
        public ConsumerOfTopicAttribute(string topic)
        {
            this.Topic = topic;
        }

        public string Topic { get; }
    }
}