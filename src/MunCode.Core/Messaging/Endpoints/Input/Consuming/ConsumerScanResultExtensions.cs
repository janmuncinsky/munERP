namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration", Justification = "Enumerable is enumerated only once.")]
    public static class ConsumerScanResultExtensions
    {
        public static void SubscribeMessages(this IEnumerable<ConsumerScanResult> enumerable, IRegisterCallbacks registerCallbacks)
        {
            Guard.NotNull(registerCallbacks, nameof(registerCallbacks));
            Guard.NotNull(enumerable, nameof(enumerable));
            foreach (var handlerScanResult in enumerable)
            {
                handlerScanResult.SubscribeMessage(registerCallbacks);
            }
        }

        public static void SubscribeMessage(this ConsumerScanResult consumerScanResult, IRegisterCallbacks registerCallbacks)
        {
            var contract = consumerScanResult.GetConsumerContract();
            registerCallbacks.SingletonRegisterCallback(typeof(IConsumerDefinition), contract.ConsumerDefinition);
            registerCallbacks.CallScopeRegisterCallback(contract.ConsumerInterface, contract.Consumer);
            registerCallbacks.CallScopeRegisterCallback(contract.MessageHandlerInterface, contract.Adapter, Guid.NewGuid().ToString());
        }
    }
}