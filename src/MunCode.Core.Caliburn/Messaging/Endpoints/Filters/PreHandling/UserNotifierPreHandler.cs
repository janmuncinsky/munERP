namespace MunCode.Core.Messaging.Endpoints.Filters.PreHandling
{
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class UserNotifierPreHandler<TMessage> : IPreHandler<TMessage>
    {
        private readonly IStatusBarViewModel statusBarViewModel;

        public UserNotifierPreHandler(IStatusBarViewModel statusBarViewModel)
        {
            Guard.NotNull(statusBarViewModel, nameof(statusBarViewModel));
            this.statusBarViewModel = statusBarViewModel;
        }

        public Task Handle(MessageContext<TMessage> context)
        {
            this.statusBarViewModel.SendMessage($"Sending message - '{typeof(TMessage).Name}'.");
            return Task.CompletedTask;
        }
    }
}