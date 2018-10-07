namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System;

    public class MessageHandlerNotFoundException : Exception
    {
        public MessageHandlerNotFoundException()
        {
        }

        public MessageHandlerNotFoundException(Exception innerException) 
            : base(string.Empty, innerException)
        {
        }
    }
}