namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System;
    using System.Threading.Tasks;

    using Caliburn.Micro;

    using MunCode.Core.Guards;

    public class DocumentHostNotifier : IDocumentHostNotifier
    {
        private readonly IEventAggregator eventAggregator;

        public DocumentHostNotifier(IEventAggregator eventAggregator)
        {
            Guard.NotNull(eventAggregator, nameof(eventAggregator));
            this.eventAggregator = eventAggregator;
        }

        public Task Notify<TMessage, TAttribute>(Type genericMessageType)
            where TAttribute : DocumentNotifierMessageAttribute
        {
            var attribute = (TAttribute)Attribute.GetCustomAttribute(typeof(TMessage), typeof(TAttribute));

            if (attribute != null)
            {
                var document = attribute.DocumentType;
                var messageType = genericMessageType.MakeGenericType(document);
                var message = Activator.CreateInstance(messageType);
                return this.eventAggregator.PublishOnUIThreadAsync(message);
            }

            return Task.CompletedTask;
        }
    }
}