namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using System;

    public interface IConsumerDefinition
    {
        Type MessageType { get; }

        Type ResponseType { get; }

        Type MessageConsumerType { get; }

        string MessageName { get; }

        string ResponseName { get; }

        string MessageConsumerName { get; }

        void Accept(IConsumerDefinitionVisitor visitor);

        Type ContainsType(string name);
    }
}