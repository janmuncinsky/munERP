namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System;

    using MunCode.Core.Guards;

    public class DocumentNotifierMessageAttribute : Attribute
    {
        public DocumentNotifierMessageAttribute(Type documentType)
        {
            Guard.NotNull(documentType, nameof(documentType));
            this.DocumentType = documentType;
        }

        public Type DocumentType { get; }
    }
}