namespace MunCode.Core.Reflection.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Messaging.Messages;
    using MunCode.Core.Reflection;

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "Enumerable is enumerated only once.")]
    public static class MessageScanResultExtensions
    {
        private delegate Type AdapterFactory(Type messageType, Type messageInterfaceType);

        public static void RedirectMessages(this IEnumerable<MessageScanResult> enumerable, IRegisterCallbacks registerCallbacks)
        {
            Guard.NotNull(registerCallbacks, nameof(registerCallbacks));
            Guard.NotNull(enumerable, nameof(enumerable));
            var adapterMap
                = new Dictionary<Type, AdapterFactory>
                      {
                          { typeof(ICommand), (mt, mit) => typeof(RedirectCommandConsumer<>).MakeGenericType(mt) },
                          { typeof(IEvent), (mt, mit) => typeof(RedirectEventConsumer<>).MakeGenericType(mt) },
                          { typeof(ITransaction<>), (mt, mit) => typeof(RedirectRequestConsumer<,>).MakeGenericType(mt, mit.GetGenericArguments()[0]) },
                          { typeof(IRequest<>), (mt, mit) => typeof(RedirectRequestConsumer<,>).MakeGenericType(mt, mit.GetGenericArguments()[0]) }
                      };

            foreach (var messageScanResult in enumerable)
            {
                var messageType = messageScanResult.Type;
                var messageInterfaces = messageType.GetInterfaces();

                foreach (var messageInterface in messageInterfaces)
                {
                    var interfaceDefinition = messageInterface.GetTypeDefinition();
                    if (adapterMap.ContainsKey(interfaceDefinition))
                    {
                        var consumer = adapterMap[interfaceDefinition](messageType, messageInterface);
                        var handlerScanResult = new RedirectConsumerScanResult(consumer);
                        handlerScanResult.SubscribeMessage(registerCallbacks);
                        break;
                    }
                }
            }
        }

        private class RedirectCommandConsumer<TCommand> : ICommandConsumer<TCommand>
            where TCommand : class, ICommand
        {
            private readonly IOutputMessageChannel channel;

            public RedirectCommandConsumer(IOutputMessageChannel channel)
            {
                Guard.NotNull(channel, nameof(channel));
                this.channel = channel;
            }

            public Task Consume(ReceiveContext<TCommand> messageContext)
            {
                Guard.NotNull(messageContext, nameof(messageContext));
                return this.channel.Send(messageContext.CreateSendContext());
            }
        }

        private class RedirectEventConsumer<TEvent> : IEventConsumer<TEvent>
            where TEvent : class, IEvent
        {
            private readonly IOutputMessageChannel channel;

            public RedirectEventConsumer(IOutputMessageChannel channel)
            {
                Guard.NotNull(channel, nameof(channel));
                this.channel = channel;
            }

            public Task Consume(ReceiveContext<TEvent> messageContext)
            {
                Guard.NotNull(messageContext, nameof(messageContext));
                return this.channel.Publish(messageContext.CreateSendContext());
            }
        }

        private class RedirectRequestConsumer<TRequest, TResponse> : IRequestConsumer<TRequest, TResponse>
            where TRequest : class, IRequest<TResponse> 
            where TResponse : class
        {
            private readonly IOutputMessageChannel channel;

            public RedirectRequestConsumer(IOutputMessageChannel channel)
            {
                Guard.NotNull(channel, nameof(channel));
                this.channel = channel;
            }

            public Task<TResponse> Consume(ReceiveContext<TRequest> messageContext)
            {
                Guard.NotNull(messageContext, nameof(messageContext));
                return this.channel.Request<TRequest, TResponse>(messageContext.CreateSendContext());
            }
        }
    }
}