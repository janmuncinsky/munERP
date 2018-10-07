namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class CommandConsumerAdapter<TCommand> : IMessageHandler<TCommand, EmptyResponse>
        where TCommand : ICommand
    {
        private readonly ICommandConsumer<TCommand> commandConsumer;

        public CommandConsumerAdapter(ICommandConsumer<TCommand> commandConsumer)
        {
            Guard.NotNull(commandConsumer, nameof(commandConsumer));
            this.commandConsumer = commandConsumer;
        }

        public async Task<EmptyResponse> Handle(ReceiveContext<TCommand> command)
        {
            Guard.NotNull(command, nameof(command));
            await this.commandConsumer.Consume(command).ConfigureAwait(false);
            return EmptyResponse.Instance;
        }
    }
}