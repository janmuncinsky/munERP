namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using System;
    using System.Collections.Generic;

    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;
    using MunCode.Core.Reflection;
    using MunCode.Core.Reflection.Scanning;

    public class ConsumerScanResult : TypeScanResult
    {
        protected readonly Dictionary<Type, EndpointFactory> ConsumerDefinitionMap
            = new Dictionary<Type, EndpointFactory>
        {
                    { typeof(ITransaction<>), (mt, ht, mit) => typeof(CommandConsumerDefinition<,>).MakeGenericType(mt, ht) },
                    { typeof(ICommand), (mt, ht, mit) => typeof(CommandConsumerDefinition<,>).MakeGenericType(mt, ht) },
                    { typeof(IEvent), (mt, ht, mit) => typeof(EventConsumerDefinition<,>).MakeGenericType(mt, ht) },
                    { typeof(IRequest<>),  (mt, ht, mit) => typeof(RequestConsumerDefinition<,,>).MakeGenericType(mt, mit.GetGenericArguments()[0], ht) }
        };

        protected readonly Dictionary<Type, AdapterFactory> MessageHandlerMap
            = new Dictionary<Type, AdapterFactory>
                  {
                      { typeof(ITransaction<>), (mt, mit) => typeof(IMessageHandler<,>).MakeGenericType(mt, typeof(EmptyResponse)) },
                      { typeof(ICommand), (mt, mit) => typeof(IMessageHandler<,>).MakeGenericType(mt, typeof(EmptyResponse)) },
                      { typeof(IEvent), (mt, mit) => typeof(IMessageHandler<,>).MakeGenericType(mt, typeof(EmptyResponse)) },
                      { typeof(IRequest<>), (mt, mit) => typeof(IMessageHandler<,>).MakeGenericType(mt, mit.GetGenericArguments()[0]) }
                  };

        protected readonly Dictionary<Type, AdapterFactory> AdapterMap
            = new Dictionary<Type, AdapterFactory>
                {
                    { typeof(ITransaction<>), (mt, mit) => typeof(CommandConsumerAdapter<>).MakeGenericType(mt) },
                    { typeof(ICommand), (mt, mit) => typeof(CommandConsumerAdapter<>).MakeGenericType(mt) },
                    { typeof(IEvent), (mt, mit) => typeof(EventConsumerAdapter<>).MakeGenericType(mt) },
                    { typeof(IRequest<>),  (mt, mit) => typeof(RequestConsumerAdapter<,>).MakeGenericType(mt, mit.GetGenericArguments()[0]) }
                };

        public ConsumerScanResult(Type consumer)
            : base(consumer)
        {
        }

        protected delegate Type EndpointFactory(Type messageType, Type handlerType, Type messageInterfaceType);

        protected delegate Type AdapterFactory(Type messageType, Type messageInterfaceType);

        public virtual ConsumerContract GetConsumerContract()
        {
            var handlerType = this.Type;

            // todo enable register multiple consumer types
            var consumerInterface = handlerType.GetInterfaces()[0];
            var handlerGenericArguments = consumerInterface.GetGenericArguments();
            var messageType = handlerGenericArguments[0];
            var messageInterfaces = messageType.GetInterfaces();

            foreach (var messageInterface in messageInterfaces)
            {
                var interfaceDefinition = messageInterface.GetTypeDefinition();

                if (this.ConsumerDefinitionMap.ContainsKey(interfaceDefinition))
                {
                    var consumerDefinition = this.ConsumerDefinitionMap[interfaceDefinition](messageType, handlerType, messageInterface);
                    var messageHandlerInterfaceType = this.MessageHandlerMap[interfaceDefinition](messageType, messageInterface);
                    var adapterType = this.AdapterMap[interfaceDefinition](messageType, messageInterface);

                    return new ConsumerContract(
                        consumerDefinition,
                        consumerInterface,
                        this.Type,
                        messageHandlerInterfaceType,
                        adapterType);
                }
            }

            throw new InvalidOperationException($"Message - of type '{messageType}' is not marked with any message interface.");
        }
    }
}