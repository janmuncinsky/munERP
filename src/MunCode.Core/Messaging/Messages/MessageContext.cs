namespace MunCode.Core.Messaging.Messages
{
    using System.Diagnostics.CodeAnalysis;

    using MunCode.Core.Guards;

    using Newtonsoft.Json;

    public class MessageContext<TMessage>
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType", Justification = "Intended")]
        private static readonly JsonSerializerSettings JsonSettings =
            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        protected MessageContext(TMessage message, MessageMetadata messageMetadata)
        {
            Guard.NotNull(messageMetadata, nameof(messageMetadata));
            Guard.NotNull(message, nameof(message));
            this.Message = message;
            this.MessageMetadata = messageMetadata;
        }

        public TMessage Message { get; }

        public MessageMetadata MessageMetadata { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, JsonSettings);
        }
    }
}