namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Memory;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters.Caching;
    using MunCode.Core.Messaging.Messages;

    public class CacheFilter<TContext, TMessage, TResponse> : IMessagePipelineFilter<TContext, TMessage, TResponse>
        where TContext : MessageContext<TMessage>
    {
        private readonly IMemoryCache cache;

        public CacheFilter(IMemoryCache cache)
        {
            Guard.NotNull(cache, nameof(cache));
            this.cache = cache;
        }

        public Task<TResponse> Handle(TContext context, MessageHandlerDelegate<TContext, TMessage, TResponse> nextHandler)
        {
            Guard.NotNull(nextHandler, nameof(nextHandler));
            Guard.NotNull(context, nameof(context));

            // todo cache types to improve performance
            var cacheAttribute = Attribute.GetCustomAttribute(typeof(TMessage), typeof(CachedAttribute)) as CachedAttribute;
            if (cacheAttribute != null)
            {
                return this.cache.GetOrCreateAsync(
                    context.Message,
                    e =>
                        {
                            e.AbsoluteExpirationRelativeToNow = cacheAttribute.Expiration;
                            return nextHandler(context);
                        });
            }

            return nextHandler(context);
        }
    }
}