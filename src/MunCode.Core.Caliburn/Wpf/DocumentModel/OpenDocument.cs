namespace MunCode.Core.Wpf.DocumentModel
{
    using System;

    using MunCode.Core.Guards;

    public class OpenDocument
    {
        public OpenDocument(Type documentType, Func<OpenedDocument> documentFactory)
        {
            Guard.NotNull(documentType, nameof(documentType));
            this.DocumentType = documentType;
            this.DocumentFactory = documentFactory;
        }

        public Func<OpenedDocument> DocumentFactory { get; }

        public Type DocumentType { get; }
    }
}