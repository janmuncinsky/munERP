namespace MunCode.Core.Messaging
{
    using MunCode.Core.Messaging.Endpoints;

    public class HttpMessageBusConfig : MessageChannelConfig
    {
        public string BaseUri { get; set; }
    }
}