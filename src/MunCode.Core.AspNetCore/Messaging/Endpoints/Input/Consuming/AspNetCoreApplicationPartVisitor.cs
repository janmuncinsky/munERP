namespace MunCode.Core.Messaging.Endpoints.Input.Consuming
{
    using System.Reflection;

    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Commands;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Events;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming.Requests;
    using MunCode.Core.Messaging.Messages;

    public class AspNetCoreApplicationPartVisitor : IConsumerDefinitionVisitor
    {
        private readonly InputMessageChannelApplicationPart part;

        public AspNetCoreApplicationPartVisitor(InputMessageChannelApplicationPart part)
        {
            this.part = part;
        }

        public void Visit<TCommand, TConsumer>(CommandConsumerDefinition<TCommand, TConsumer> commandConsumerDefinition)
            where TCommand : class, ICommand 
            where TConsumer : ICommandConsumer<TCommand>
        {
            var type = typeof(InputMessageChannelController<,>).MakeGenericType(typeof(TCommand), typeof(EmptyResponse));
            this.part.AddType(type.GetTypeInfo());
        }

        public void Visit<TEvent, TConsumer>(EventConsumerDefinition<TEvent, TConsumer> eventConsumerDefinition)
            where TEvent : class, IEvent 
            where TConsumer : IEventConsumer<TEvent>
        {
            var type = typeof(InputMessageChannelController<,>).MakeGenericType(typeof(TEvent), typeof(EmptyResponse));
            this.part.AddType(type.GetTypeInfo());
        }

        public void Visit<TRequest, TResponse, TConsumer>(RequestConsumerDefinition<TRequest, TResponse, TConsumer> requestConsumerDefinition)
            where TRequest : class, IRequest<TResponse> 
            where TResponse : class 
            where TConsumer : IRequestConsumer<TRequest, TResponse>
        {
            var type = typeof(InputMessageChannelController<,>).MakeGenericType(typeof(TRequest), typeof(TResponse));
            this.part.AddType(type.GetTypeInfo());
        }
    }
}