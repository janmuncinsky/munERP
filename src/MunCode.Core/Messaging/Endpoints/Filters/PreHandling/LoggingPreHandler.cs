namespace MunCode.Core.Messaging.Endpoints.Filters.PreHandling
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class LoggingPreHandler<TMessage> : IPreHandler<TMessage>
    {
        private readonly ILogger logger;

        public LoggingPreHandler(ILogger<TMessage> logger)
        {
            Guard.NotNull(logger, nameof(logger));
            this.logger = logger;
        }

        public Task Handle(MessageContext<TMessage> context)
        {
            Guard.NotNull(context, nameof(context));
            this.logger.LogInformation("About to handle a message - '{context}'.", context);
            return Task.CompletedTask;
        }
    }
}