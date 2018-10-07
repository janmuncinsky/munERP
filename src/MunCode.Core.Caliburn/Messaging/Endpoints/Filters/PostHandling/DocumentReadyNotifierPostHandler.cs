namespace MunCode.Core.Messaging.Endpoints.Filters.PostHandling
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class DocumentReadyNotifierPostHandler<TMessage, TResponse> : IPostHandler<TMessage, TResponse>
    {
        private readonly IDocumentHostNotifier documentHostNotifier;

        public DocumentReadyNotifierPostHandler(IDocumentHostNotifier documentHostNotifier)
        {
            Guard.NotNull(documentHostNotifier, nameof(documentHostNotifier));
            this.documentHostNotifier = documentHostNotifier;
        }

        public Task Handle(MessageContext<TMessage> context, TResponse response)
        {
            return this.documentHostNotifier.Notify<TMessage, BusyNotifierMessageAttribute>(typeof(DocumentReady<>));
        }
    }
}