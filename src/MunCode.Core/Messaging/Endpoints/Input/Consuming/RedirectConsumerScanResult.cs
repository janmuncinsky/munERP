namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using System;

    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;

    public class RedirectConsumerScanResult : ConsumerScanResult
    {
        public RedirectConsumerScanResult(Type consumer)
            : base(consumer)
        {
            this.ConsumerDefinitionMap[typeof(ITransaction<>)] = (mt, ht, mit) => typeof(RequestConsumerDefinition<,,>).MakeGenericType(mt, mit.GetGenericArguments()[0], ht);
            this.MessageHandlerMap[typeof(ITransaction<>)] = (mt, mit) => typeof(IMessageHandler<,>).MakeGenericType(mt, mit.GetGenericArguments()[0]);
            this.AdapterMap[typeof(ITransaction<>)] = (mt, mit) => typeof(RequestConsumerAdapter<,>).MakeGenericType(mt, mit.GetGenericArguments()[0]);
        }
    }
}