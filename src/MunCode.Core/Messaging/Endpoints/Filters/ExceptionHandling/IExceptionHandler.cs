namespace MunCode.Core.Messaging.Endpoints.Filters.ExceptionHandling
{
    using System;
    using System.Threading.Tasks;

    using MunCode.Core.Messaging.Messages;

    public interface IExceptionHandler<TMessage>
    {
        Task Handle(MessageContext<TMessage> context, Exception exception);
    }
}