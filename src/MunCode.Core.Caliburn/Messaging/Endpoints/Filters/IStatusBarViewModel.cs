namespace MunCode.Core.Messaging.Endpoints.Filters
{
    public interface IStatusBarViewModel
    {
        string Text { get; }

        void SendMessage(string message);
    }
}
