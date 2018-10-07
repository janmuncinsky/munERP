namespace MunCode.Core.Ioc
{
    using System;

    using Castle.Core;
    using Castle.MicroKernel.Context;
    using Castle.MicroKernel.Handlers;

    using MunCode.Core.Messaging.Endpoints.Filters;

    public class ReceiveContextMatchingStrategy : IGenericImplementationMatchingStrategy
    {
        public Type[] GetGenericArguments(ComponentModel model, CreationContext context)
        {
            if (context.RequestedType.IsGenericType && context.RequestedType.GetGenericTypeDefinition() == typeof(IMessagePipelineFilter<,,>))
            {
                var serviceArguments = context.RequestedType.GetGenericArguments();
                return new[] { serviceArguments[1], serviceArguments[2] };
            }

            throw new InvalidOperationException($"Strategy is not applicable for requested type '{context.RequestedType}'.");
        }
    }
}