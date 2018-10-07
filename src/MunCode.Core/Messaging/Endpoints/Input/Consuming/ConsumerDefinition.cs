namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using System;

    public abstract class ConsumerDefinition<TMessage, TResponse, TConsumer> : IConsumerDefinition
    {
        public Type MessageType => typeof(TMessage);

        public Type ResponseType => typeof(TResponse);

        public Type MessageConsumerType => typeof(TConsumer);

        public string MessageName => typeof(TMessage).Name;

        public string ResponseName => typeof(TResponse).Name;

        public string MessageConsumerName => typeof(TConsumer).FullName;

        public abstract void Accept(IConsumerDefinitionVisitor visitor);

        public Type ContainsType(string name)
        {
            if (this.MessageName == name)
            {
                return this.MessageType;
            }

            if (this.ResponseName == name)
            {
                return this.ResponseType;
            }

            return null;
        }
    }
}