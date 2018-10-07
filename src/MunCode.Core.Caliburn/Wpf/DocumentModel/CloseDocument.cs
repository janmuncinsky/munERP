namespace MunCode.Core.Wpf.DocumentModel
{
    using System;

    public class CloseDocument
    {
        public CloseDocument(Type documentType)
        {
            this.DocumentType = documentType;
        }

        public Type DocumentType { get; }
    }
}