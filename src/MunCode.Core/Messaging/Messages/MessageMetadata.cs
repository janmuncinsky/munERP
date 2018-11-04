namespace MunCode.Core.Messaging.Messages
{
    using System;

    public class MessageMetadata
    {
        public MessageMetadata()
        {
        }

        public MessageMetadata(string replyTo = null, Guid? correlationId = null, string topic = null)
        {
            this.ReplyTo = replyTo;
            this.CorrelationId = correlationId;
            this.Topic = topic;
        }

        public MessageMetadata(string replyTo = null, string correlationId = null, string topic = null)
            : this(replyTo, correlationId == null ? (Guid?)null : Guid.Parse(correlationId), topic)
        {
        }

        public string ReplyTo { get; }

        public string Topic { get; }

        public Guid? CorrelationId { get; }
    }
}