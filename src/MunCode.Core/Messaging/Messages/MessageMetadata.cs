namespace MunCode.Core.Messaging.Messages
{
    using System;

    public class MessageMetadata
    {
        public MessageMetadata()
        {
        }

        public MessageMetadata(string replyTo, Guid? correlationId)
        {
            this.ReplyTo = replyTo;
            this.CorrelationId = correlationId;
        }

        public MessageMetadata(string replyTo, string correlationId)
        {
            this.ReplyTo = replyTo;
            this.CorrelationId = correlationId == null ? (Guid?)null : Guid.Parse(correlationId);
        }

        public string ReplyTo { get; }

        public Guid? CorrelationId { get; }
    }
}