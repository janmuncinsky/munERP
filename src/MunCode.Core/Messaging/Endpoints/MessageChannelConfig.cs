namespace MunCode.Core.Messaging.Endpoints
{
    public abstract class MessageChannelConfig : IMessageChannelConfig
    {
        public string[] Messages { get; set; } = { };
    }
}