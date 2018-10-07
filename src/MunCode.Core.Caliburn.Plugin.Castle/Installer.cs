namespace MunCode.Core.Caliburn.Plugin.Castle
{
    using global::Caliburn.Micro;

    using global::Castle.Facilities.TypedFactory;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using MunCode.Core.AppHosting;
    using MunCode.Core.Messaging.Endpoints.Filters;
    using MunCode.Core.Messaging.Endpoints.Filters.ExceptionHandling;
    using MunCode.Core.Messaging.Endpoints.Filters.PostHandling;
    using MunCode.Core.Messaging.Endpoints.Filters.PreHandling;
    using MunCode.Core.Wpf.DialogService;
    using MunCode.Core.Wpf.DocumentModel;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IDocumentHost<>)).ImplementedBy(typeof(DocumentHost<>)).LifestyleScoped(),
                Component.For<IWindowManager>().ImplementedBy<WindowManager>().LifestyleSingleton(),
                Component.For<IWindowFactory>().AsFactory().LifestyleSingleton(),
                Component.For<IWindowService, IDialogService, IMessageBoxService>().ImplementedBy<DialogService>().LifestyleSingleton(),
                Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton(),
                Component.For<IDocumentHostNotifier>().ImplementedBy<DocumentHostNotifier>().LifestyleSingleton(),
                Component.For(typeof(IPreHandler<>)).ImplementedBy(typeof(UserNotifierPreHandler<>)).LifestyleSingleton(),
                Component.For(typeof(IPreHandler<>)).ImplementedBy(typeof(DocumentBusyNotifierPreHandler<>)).LifestyleSingleton(),
                Component.For(typeof(IPostHandler<,>)).ImplementedBy(typeof(UserNotifierPostHandler<,>)).LifestyleSingleton(),
                Component.For(typeof(IPostHandler<,>)).ImplementedBy(typeof(DocumentReadyNotifierPostHandler<,>)).LifestyleSingleton(),
                Component.For(typeof(IPostHandler<,>)).ImplementedBy(typeof(DocumentClosedNotifierPostHandler<,>)).LifestyleSingleton(),
                Component.For(typeof(IExceptionHandler<>)).ImplementedBy(typeof(UserNotifierExceptionHandler<>)).LifestyleSingleton(),
                Component.For(typeof(IExceptionHandler<>)).ImplementedBy(typeof(DocumentReadyNotifierExceptionHandler<>)).LifestyleSingleton(),
                Component.For<IAppInitializer>().ImplementedBy<ViewModelAppInitializer>());
        }
    }
}