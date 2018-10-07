namespace MunCode.mERP.Client.Win.Shell.UI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Caliburn.Micro;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Wpf.DocumentModel;
    using MunCode.Core.Wpf.DocumentModel.Menu;

    public class ShellViewModel : IHandle<OpenDocument>, IHandle<CloseDocument>
    {
        private readonly ObservableCollection<OpenedDocument> documents;

        public ShellViewModel(IStatusBarViewModel statusBarViewModel, IEventAggregator eventAggregator, ICollection<IMenuComponent> menuItems)
        {
            Guard.NotNull(statusBarViewModel, nameof(statusBarViewModel));
            Guard.NotNull(eventAggregator, nameof(eventAggregator));
            Guard.NotNull(menuItems, nameof(menuItems));

            this.StatusBarViewModel = statusBarViewModel;
            this.MenuItems = menuItems;
            this.documents = new ObservableCollection<OpenedDocument>();
            eventAggregator.Subscribe(this);
        }

        public IEnumerable<OpenedDocument> Documents => this.documents;

        public IEnumerable<IMenuComponent> MenuItems { get; }

        public IStatusBarViewModel StatusBarViewModel { get; }

        public void Handle(OpenDocument message)
        {
            var openedDocument = this.FindOpenedDocument(message.DocumentType);
            if (openedDocument == null)
            {
                openedDocument = message.DocumentFactory();
                this.documents.Add(openedDocument);
            }

            openedDocument.DocumentHost.IsSelected = true;
        }

        public void Handle(CloseDocument message)
        {
            var openedDocument = this.FindOpenedDocument(message.DocumentType);
            if (openedDocument != null)
            {
                this.documents.Remove(openedDocument);
                openedDocument.Scope.Dispose();
            }
        }

        private OpenedDocument FindOpenedDocument(Type documentType)
        {
            return this.documents.FirstOrDefault(d => d.DocumentHost.IsHostOfDocument(documentType));
        }
    }
}
