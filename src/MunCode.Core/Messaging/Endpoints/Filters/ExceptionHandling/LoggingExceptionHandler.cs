namespace MunCode.Core.Messaging.Endpoints.Filters.ExceptionHandling
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class LoggingExceptionHandler<TMessage> : IExceptionHandler<TMessage>
    {
        private readonly ILogger<TMessage> logger;

        public LoggingExceptionHandler(ILogger<TMessage> logger)
        {
            Guard.NotNull(logger, nameof(logger));
            this.logger = logger;
        }

        public Task Handle(MessageContext<TMessage> context, Exception exception)
        {
            Guard.NotNull(exception, nameof(exception));
            Guard.NotNull(context, nameof(context));
            this.logger.LogError(exception, "Failed to handle a message.");
            return Task.CompletedTask;
        }
    }
}