namespace MunCode.Core.Wpf.DocumentModel.Menu
{
    using System;

    using global::Caliburn.Micro;

    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;
    using MunCode.Core.Wpf.DialogService;

    public abstract class MenuComponent<TDocument> : MenuComponent
        where TDocument : IDocument
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IWindowFactory windowFactory;
        private readonly ICallScopeFactory callScopeFactory;

        protected MenuComponent(IEventAggregator eventAggregator, IWindowFactory windowFactory, ICallScopeFactory callScopeFactory)
        {
            Guard.NotNull(callScopeFactory, nameof(callScopeFactory));
            Guard.NotNull(eventAggregator, nameof(eventAggregator));
            Guard.NotNull(windowFactory, nameof(windowFactory));

            this.eventAggregator = eventAggregator;
            this.windowFactory = windowFactory;
            this.callScopeFactory = callScopeFactory;
        }

        public override void OpenScreen()
        {
            OpenedDocument DocumentFactory()
            {
                var scope = this.callScopeFactory.CreateScope();
                var host = (IDocumentHost<IDocument>)this.windowFactory.Create<IDocumentHost<TDocument>>();
                return new OpenedDocument(host, scope);
            }

            var message = new OpenDocument(typeof(TDocument), DocumentFactory);
            this.eventAggregator.PublishOnUIThread(message);
        }
    }
}