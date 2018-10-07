namespace MunCode.Core.Messaging.Endpoints.Filters.ExceptionHandling
{
    using System;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters.PostHandling;
    using MunCode.Core.Messaging.Messages;

    public class DocumentReadyNotifierExceptionHandler<TMessage> : IExceptionHandler<TMessage>
    {
        private readonly IDocumentHostNotifier documentHostNotifier;

        public DocumentReadyNotifierExceptionHandler(IDocumentHostNotifier documentHostNotifier)
        {
            Guard.NotNull(documentHostNotifier, nameof(documentHostNotifier));
            this.documentHostNotifier = documentHostNotifier;
        }

        public Task Handle(MessageContext<TMessage> context, Exception exception)
        {
            return this.documentHostNotifier.Notify<TMessage, BusyNotifierMessageAttribute>(typeof(DocumentReady<>));
        }
    }
}