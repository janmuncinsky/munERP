namespace MunCode.Core.Messaging.Endpoints
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Output;

    public class MessageChannelConfigurator<TChannelConfig> : IMessageChannelConfigurator
        where TChannelConfig : class, IMessageChannelConfig, new()
    {
        private readonly TChannelConfig config;
        private readonly ILogger<TChannelConfig> logger;
        private HashSet<string> collectionOfMessageNames;

        public MessageChannelConfigurator(IOptions<TChannelConfig> config, ILogger<TChannelConfig> logger)
        {
            Guard.NotNull(logger, nameof(logger));
            Guard.NotNull(config, nameof(config));
            this.config = config.Value;
            this.logger = logger;
        }

        private HashSet<string> CollectionOfMessageNames => this.collectionOfMessageNames ??
                                                            (this.collectionOfMessageNames = new HashSet<string>(this.config.Messages));

        public bool CanRoute(Type messageType)
        {
            Guard.NotNull(messageType, nameof(messageType));
            var typeName = messageType.Name;
            if (this.CollectionOfMessageNames.Contains(typeName))
            {
                this.logger.LogInformation($"Establishing channel for message type '{messageType.FullName}'.");
                return true;
            }

            return false;
        }
    }
}