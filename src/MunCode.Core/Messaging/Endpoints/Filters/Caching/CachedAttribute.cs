namespace MunCode.Core.Messaging.Endpoints.Filters.Caching
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class CachedAttribute : Attribute
    {
        public CachedAttribute()
        {
        }

        public CachedAttribute(TimeSpan expiration)
        {
            this.Expiration = expiration;
        }

        public TimeSpan? Expiration { get; }
    }
}