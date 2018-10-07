namespace MunCode.Core.Messaging.Endpoints.Filters.PreHandling
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class DocumentBusyNotifierPreHandler<TMessage> : IPreHandler<TMessage>
    {
        private readonly IDocumentHostNotifier documentHostNotifier;

        public DocumentBusyNotifierPreHandler(IDocumentHostNotifier documentHostNotifier)
        {
            Guard.NotNull(documentHostNotifier, nameof(documentHostNotifier));
            this.documentHostNotifier = documentHostNotifier;
        }

        public Task Handle(MessageContext<TMessage> context)
        {
            return this.documentHostNotifier.Notify<TMessage, BusyNotifierMessageAttribute>(typeof(DocumentBusy<>));
        }
    }
}