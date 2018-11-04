namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using System;

    public interface ITopicDispatcher
    {
        bool CanConsumeTopic(Type consumerType, string topic);
    }
}