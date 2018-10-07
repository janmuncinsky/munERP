namespace MunCode.Core.Wpf.DocumentModel
{
    using System;

    public interface IDocumentHostFactory
    {
        IDocumentHost<TDocument> Create<TDocument>(IDisposable scope) where TDocument : IDocument;
    }
}