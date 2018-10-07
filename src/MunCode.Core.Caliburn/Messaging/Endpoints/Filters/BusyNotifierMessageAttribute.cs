namespace MunCode.Core.Messaging.Endpoints.Filters
{
    using System;

    public class BusyNotifierMessageAttribute : DocumentNotifierMessageAttribute
    {
        public BusyNotifierMessageAttribute(Type documentType)
            : base(documentType)
        {
        }
    }
}