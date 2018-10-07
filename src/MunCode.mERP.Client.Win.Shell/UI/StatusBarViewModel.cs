namespace MunCode.mERP.Client.Win.Shell.UI
{
    using Caliburn.Micro;

    using MunCode.Core.Messaging.Endpoints.Filters;

    public class StatusBarViewModel : PropertyChangedBase, IStatusBarViewModel
    {
        private string text;

        public string Text
        {
            get => this.text;

            private set
            {
                this.text = value;
                this.NotifyOfPropertyChange(nameof(this.Text));
            }
        }

        public void SendMessage(string message)
        {
            this.Text = string.Empty;
            this.Text = message;
        }
    }
}
