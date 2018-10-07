namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System;
    using System.Threading.Tasks;

    public interface IDocumentHostNotifier
    {
        Task Notify<TMessage, TAttribute>(Type genericMessageType)
            where TAttribute : DocumentNotifierMessageAttribute;
    }
}