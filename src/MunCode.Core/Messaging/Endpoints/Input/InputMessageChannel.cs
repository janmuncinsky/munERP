namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class InputMessageChannel : IInputMessageChannel
    {
        private readonly IMessageHandlerFactory factory;
        private readonly IMessagePipeLine messagePipeLine;

        public InputMessageChannel(IMessagePipeLine messagePipeLine, IMessageHandlerFactory factory)
        {
            Guard.NotNull(factory, nameof(factory));
            Guard.NotNull(messagePipeLine, nameof(messagePipeLine));
            this.messagePipeLine = messagePipeLine;
            this.factory = factory;
        }

        public Task<TResponse> Dispatch<TMessage, TResponse>(ReceiveContext<TMessage> context)
        {
            Guard.NotNull(context, nameof(context));

            Task<TResponse> EndDelegate(ReceiveContext<TMessage> c)
            {
                try
                {
                    return this.factory.Create<TMessage, TResponse>().Handle(c);
                }
                catch (Exception e)
                {
                    throw new MessageHandlerNotFoundException(e);
                }
            }

            return this.messagePipeLine.Dispatch<ReceiveContext<TMessage>, TMessage, TResponse>(context, EndDelegate);
        }
    }
}