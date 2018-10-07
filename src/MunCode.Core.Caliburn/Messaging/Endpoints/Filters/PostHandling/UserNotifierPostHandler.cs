namespace MunCode.Core.Messaging.Endpoints.Filters.PostHandling
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class UserNotifierPostHandler<TMessage, TResponse> : IPostHandler<TMessage, TResponse>
    {
        private readonly IStatusBarViewModel statusBarViewModel;

        public UserNotifierPostHandler(IStatusBarViewModel statusBarViewModel)
        {
            Guard.NotNull(statusBarViewModel, nameof(statusBarViewModel));
            this.statusBarViewModel = statusBarViewModel;
        }

        public Task Handle(MessageContext<TMessage> context, TResponse response)
        {
            this.statusBarViewModel.SendMessage(string.Empty);
            return Task.CompletedTask;
        }
    }
}