namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using MunCode.Core.Controllers;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    // [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [FirstGenericArgumentNamingConvention]
    public class InputMessageChannelController<TMessage, TResponse> : Controller
    {
        private readonly IInputMessageChannel channel;
        private readonly IMessageContextFactory messageContextFactory;

        public InputMessageChannelController(IInputMessageChannel channel, IMessageContextFactory messageContextFactory)
        {
            Guard.NotNull(messageContextFactory, nameof(messageContextFactory));
            Guard.NotNull(channel, nameof(channel));
            this.channel = channel;
            this.messageContextFactory = messageContextFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Dispatch([FromBody] TMessage message)
        {
            Guard.NotNull(message, nameof(message));
            try
            {
                var messageMetadata = new MessageMetadata();
                var context = this.messageContextFactory.Create<ReceiveContext<TMessage>, TMessage>(message, messageMetadata);
                var response = await this.channel.Dispatch<TMessage, TResponse>(context);
                return this.Ok(response);
            }
            catch (MessageHandlerNotFoundException)
            {
                return this.BadRequest();
            }
        }
    }
}