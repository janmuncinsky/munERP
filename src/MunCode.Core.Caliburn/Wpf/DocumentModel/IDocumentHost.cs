namespace MunCode.Core.Wpf.DocumentModel
{
    using System;

    public interface IDocumentHost<out TDocument> where TDocument : IDocument
    {
        string DisplayName { get; }

        TDocument Document { get; }

        bool IsExecuting { get; }

        bool IsSelected { get; set; }

        bool IsHostOfDocument(Type type);
    }
}