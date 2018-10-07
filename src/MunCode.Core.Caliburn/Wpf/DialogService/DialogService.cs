namespace MunCode.Core.Wpf.DialogService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;

    using global::Caliburn.Micro;

    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;
    using MunCode.Core.Wpf.DocumentModel;

    public class DialogService : IWindowService
    {
        private readonly IWindowManager windowManager;
        private readonly IWindowFactory windowFactory;
        private readonly ICallScopeFactory callScopeFactory;

        public DialogService(IWindowManager windowManager, IWindowFactory windowFactory, ICallScopeFactory callScopeFactory)
        {
            Guard.NotNull(callScopeFactory, nameof(callScopeFactory));
            Guard.NotNull(windowManager, nameof(windowManager));
            Guard.NotNull(windowFactory, nameof(windowFactory));
            this.windowManager = windowManager;
            this.windowFactory = windowFactory;
            this.callScopeFactory = callScopeFactory;
        }

        public async Task<TDialogResult> ShowDialog<TDialogResult>(object context = null, IDictionary<string, object> settings = null)
        {
            using (this.callScopeFactory.CreateScope())
            {
                var dialogModel = this.windowFactory.Create<IHaveDialogResult<TDialogResult>>();
                if (dialogModel is ICanBeInitialized canBeInitialized)
                {
                    await canBeInitialized.Initialize();
                }

                var result = this.windowManager.ShowDialog(dialogModel, context, settings);
                return result ?? false ? dialogModel.DialogResult : default(TDialogResult);
            }
        }

        public MessageBoxResult ShowMessageBox(string messageBoxText)
        {
            return MessageBox.Show(messageBoxText);
        }
    }
}
