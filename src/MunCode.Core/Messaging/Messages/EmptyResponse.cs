namespace MunCode.Core.Messaging.Messages
{
    using MunCode.Core.Design.Patterns.Gof.Singleton;

    public class EmptyResponse : Singleton<EmptyResponse>
    {
        private EmptyResponse()
        {
        }
    }
}