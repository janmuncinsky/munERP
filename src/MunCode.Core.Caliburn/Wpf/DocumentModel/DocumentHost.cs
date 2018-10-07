namespace MunCode.Core.Wpf.DocumentModel
{
    using System;

    using global::Caliburn.Micro;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Filters.PostHandling;
    using MunCode.Core.Messaging.Endpoints.Filters.PreHandling;

    using ICommand = System.Windows.Input.ICommand;

    public sealed class DocumentHost<TDocument> 
        : PropertyChangedBase, 
          IDocumentHost<TDocument>,
          IHandle<DocumentBusy<TDocument>>,
          IHandle<DocumentReady<TDocument>>,
          IHandle<DocumentClosed<TDocument>>
        where TDocument : IDocument
    {
        private readonly IEventAggregator eventAggregator;

        private bool isSelected;
        private bool isExecuting;

        public DocumentHost(IEventAggregator eventAggregator, TDocument document)
        {
            Guard.NotNull(eventAggregator, nameof(eventAggregator));
            Guard.NotNull(document, nameof(document));

            this.eventAggregator = eventAggregator;
            this.Document = document;
            this.CloseDocumentCommand = new RelayCommand(o => this.CloseDocument());
            eventAggregator.Subscribe(this);
        }

        public string DisplayName => this.Document.DisplayName;

        public TDocument Document { get; }

        public ICommand CloseDocumentCommand { get; }

        public bool IsExecuting
        {
            get => this.isExecuting;

            private set
            {
                this.isExecuting = value;
                this.NotifyOfPropertyChange(nameof(this.IsExecuting));
            }
        }

        public bool IsSelected
        {
            get => this.isSelected;

            set
            {
                this.Initialize(this.isSelected);
                this.isSelected = value;
                this.NotifyOfPropertyChange(nameof(this.IsSelected));
            }
        }

        public bool IsHostOfDocument(Type type)
        {
            return typeof(TDocument) == type;
        }

        public void Handle(DocumentBusy<TDocument> message)
        {
            this.IsExecuting = true;
        }

        public void Handle(DocumentReady<TDocument> message)
        {
            this.IsExecuting = false;
        }

        public void Handle(DocumentClosed<TDocument> message)
        {
            this.CloseDocument();
        }

        private async void Initialize(bool wasSelected)
        {
            if (this.Document is ICanBeInitialized canBeInitialized && !wasSelected)
            {
                try
                {
                    await canBeInitialized.Initialize();
                }
                catch (Exception)
                {
                    this.CloseDocument();
                }
            }
        }

        private void CloseDocument()
        {
            this.eventAggregator.PublishOnUIThread(new CloseDocument(typeof(TDocument)));
        }
    }
}