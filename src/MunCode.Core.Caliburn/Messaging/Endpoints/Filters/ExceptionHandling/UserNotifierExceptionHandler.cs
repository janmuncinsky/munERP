namespace MunCode.Core.Messaging.Endpoints.Filters.ExceptionHandling
{
    using System;
    using System.Threading.Tasks;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Messages;

    public class UserNotifierExceptionHandler<TMessage> : IExceptionHandler<TMessage>
    {
        private readonly IStatusBarViewModel statusBarViewModel;

        public UserNotifierExceptionHandler(IStatusBarViewModel statusBarViewModel)
        {
            Guard.NotNull(statusBarViewModel, nameof(statusBarViewModel));
            this.statusBarViewModel = statusBarViewModel;
        }

        public Task Handle(MessageContext<TMessage> context, Exception exception)
        {
            this.statusBarViewModel.SendMessage($"Sending of message - '{typeof(TMessage).Name}' failed.");
            return Task.CompletedTask;
        }
    }
}