namespace MunCode.Core.Ioc
{
    using System;

    using Castle.Core;
    using Castle.MicroKernel.Handlers;

    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Messages;

    public class ReceiveContextServiceStrategy : IGenericServiceStrategy
    {
        public bool Supports(Type service, ComponentModel component)
        {
            return service.IsGenericType && 
                   service.GetGenericTypeDefinition() == typeof(IMessagePipelineFilter<,,>) && 
                   service.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(ReceiveContext<>);
        }
    }
}