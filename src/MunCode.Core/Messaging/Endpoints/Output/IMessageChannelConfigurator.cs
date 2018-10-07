namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System;

    public interface IMessageChannelConfigurator
    {
        bool CanRoute(Type messageType);
    }
}