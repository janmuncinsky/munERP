namespace MunCode.Core.Wpf.DocumentModel
{
    using System;

    public class OpenedDocument
    {
        public OpenedDocument(IDocumentHost<IDocument> documentHost, IDisposable scope)
        {
            this.DocumentHost = documentHost;
            this.Scope = scope;
        }

        public IDocumentHost<IDocument> DocumentHost { get; }

        public IDisposable Scope { get; }
    }
}