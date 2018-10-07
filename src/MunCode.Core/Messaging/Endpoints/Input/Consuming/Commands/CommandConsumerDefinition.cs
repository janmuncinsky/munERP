namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands
{
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class CommandConsumerDefinition<TCommand, TConsumer> : ConsumerDefinition<TCommand, EmptyResponse, TConsumer>
        where TCommand : class, ICommand
        where TConsumer : ICommandConsumer<TCommand>
    {
        public override void Accept(IConsumerDefinitionVisitor visitor)
        {
            Guard.NotNull(visitor, nameof(visitor));
            visitor.Visit(this);
        }
    }
}