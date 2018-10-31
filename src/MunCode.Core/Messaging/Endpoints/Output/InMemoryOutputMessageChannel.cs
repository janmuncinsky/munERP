namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System;
    using System.Threading.Tasks;

    using MunCode.Core.Design.Domain;
    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input;
    using MunCode.Core.Messaging.Messages;

    public class InMemoryOutputMessageChannel : IOutputMessageChannel
    {
        private readonly IInputMessageChannel inputMessageChannel;

        public InMemoryOutputMessageChannel(IInputMessageChannel inputMessageChannel)
        {
            Guard.NotNull(inputMessageChannel, nameof(inputMessageChannel));
            this.inputMessageChannel = inputMessageChannel;
        }

        public Task Send<TCommand>(SendContext<TCommand> context)
            where TCommand : class, ICommand
        {
            return this.inputMessageChannel.Dispatch<TCommand, EmptyResponse>(this.CreateContext(context), this.GetTopic(context.Message));
        }

        public Task Publish<TEvent>(SendContext<TEvent> context)
            where TEvent : class, IEvent
        {
            return this.inputMessageChannel.Dispatch<TEvent, EmptyResponse>(this.CreateContext(context), this.GetTopic(context.Message));
        }

        public Task<TResponse> Request<TRequest, TResponse>(SendContext<TRequest> context)
            where TRequest : class, IRequest<TResponse> 
            where TResponse : class
        {
            return this.inputMessageChannel.Dispatch<TRequest, TResponse>(this.CreateContext(context), this.GetTopic(context.Message));
        }

        public Task Respond<TResponse>(SendContext<TResponse> context)
            where TResponse : class
        {
            return this.inputMessageChannel.Dispatch<TResponse, EmptyResponse>(this.CreateContext(context), this.GetTopic(context.Message));
        }

        private string GetTopic<TMessage>(TMessage message)
        {
            if (message is IHaveTopic topicMessage)
            {
                return topicMessage.Topic;
            }

            return string.Empty;
        }

        private ReceiveContext<TMessage> CreateContext<TMessage>(SendContext<TMessage> context)
        {
            Guard.NotNull(context, nameof(context));
            return context.CreateReceiveContext();
        }
    }
}