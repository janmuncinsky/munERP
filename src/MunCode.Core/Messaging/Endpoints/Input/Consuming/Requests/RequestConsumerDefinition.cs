namespace MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests
{
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class RequestConsumerDefinition<TRequest, TResponse, TConsumer> : ConsumerDefinition<TRequest, TResponse, TConsumer>
        where TRequest : class, IRequest<TResponse>
        where TResponse : class
        where TConsumer : IRequestConsumer<TRequest, TResponse>
    {
        public override void Accept(IConsumerDefinitionVisitor visitor)
        {
            Guard.NotNull(visitor, nameof(visitor));
            visitor.Visit(this);
        }
    }
}