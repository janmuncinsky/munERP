namespace MunCode.Core.Messaging.Endpoints.Filters.PostHandling
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class LoggingPostHandler<TMessage, TResponse> : IPostHandler<TMessage, TResponse>
    {
        private readonly ILogger logger;

        public LoggingPostHandler(ILogger<TMessage> logger)
        {
            Guard.NotNull(logger, nameof(logger));
            this.logger = logger;
        }

        public Task Handle(MessageContext<TMessage> context, TResponse response)
        {
            Guard.NotNull(response, nameof(response));
            Guard.NotNull(context, nameof(context));
            this.logger.LogInformation("Message successfully handled with response {0}.", response);
            return Task.CompletedTask;
        }
    }
}