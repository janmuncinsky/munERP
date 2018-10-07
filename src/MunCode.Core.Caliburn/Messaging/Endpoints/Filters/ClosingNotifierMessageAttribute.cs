namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System;

    public class ClosingNotifierMessageAttribute : DocumentNotifierMessageAttribute
    {
        public ClosingNotifierMessageAttribute(Type documentType)
            : base(documentType)
        {
        }
    }
}