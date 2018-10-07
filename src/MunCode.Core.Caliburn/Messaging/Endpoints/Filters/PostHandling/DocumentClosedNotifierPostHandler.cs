namespace MunCode.Core.Messaging.Endpoints.Filters.PostHandling
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class DocumentClosedNotifierPostHandler<TMessage, TResponse> : IPostHandler<TMessage, TResponse>
    {
        private readonly IDocumentHostNotifier documentHostNotifier;

        public DocumentClosedNotifierPostHandler(IDocumentHostNotifier documentHostNotifier)
        {
            this.documentHostNotifier = documentHostNotifier;
            Guard.NotNull(documentHostNotifier, nameof(documentHostNotifier));
        }

        public Task Handle(MessageContext<TMessage> context, TResponse response)
        {
            return this.documentHostNotifier.Notify<TMessage, ClosingNotifierMessageAttribute>(typeof(DocumentClosed<>));
        }
    }
}