namespace MunCode.Core.Reflection.Scanning
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "Enumerable is enumerated only once.")]
    public static class MessagingAssemblyScanResultEnumerableExtensions
    {
        private static readonly IEnumerable<Type> ConsumerTypes =
            new[] { typeof(ICommandConsumer<>), typeof(IEventConsumer<>), typeof(IRequestConsumer<,>) };

        private static readonly IEnumerable<Type> MessageTypes =
            new[] { typeof(ICommand), typeof(IEvent), typeof(IRequest<>) };

        public static IEnumerable<ConsumerScanResult> ScanForMessageConsumers(
            this IEnumerable<AssemblyScanResult> enumerable)
        {
            Guard.NotNull(enumerable, nameof(enumerable));
            foreach (var consumer in ScanFor(enumerable, ConsumerTypes))
            {
                yield return new ConsumerScanResult(consumer.Type);
            }
        }

        public static IEnumerable<MessageScanResult> ScanForMessages(this IEnumerable<AssemblyScanResult> enumerable)
        {
            Guard.NotNull(enumerable, nameof(enumerable));
            foreach (var message in ScanFor(enumerable, MessageTypes))
            {
                yield return new MessageScanResult(message.Type);
            }
        }

        private static IEnumerable<TypeScanResult> ScanFor(
            IEnumerable<AssemblyScanResult> enumerable,
            IEnumerable<Type> supportedInterfaces)
        {
            Guard.NotNull(supportedInterfaces, nameof(supportedInterfaces));
            Guard.NotNull(enumerable, nameof(enumerable));
            var supportedInterfacesSet = new HashSet<Type>(supportedInterfaces);
            return enumerable.ScanForTypes(
                t => !t.IsAbstract && t.GetInterfaces().Any(
                    x => supportedInterfacesSet.Contains(x)
                         || (x.IsGenericType && supportedInterfacesSet.Contains(x.GetGenericTypeDefinition()))));
        }
    }
}