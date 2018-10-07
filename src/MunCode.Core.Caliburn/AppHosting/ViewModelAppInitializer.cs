namespace MunCode.Core.AppHosting
{
    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;
    using MunCode.Core.Refection.Scanning;
    using MunCode.Core.Reflection.Scanning;
    using MunCode.Core.Wpf;

    public class ViewModelAppInitializer : IAppInitializer
    {
        private readonly IRegisterCallbacks registerCallbacks;

        public ViewModelAppInitializer(IRegisterCallbacks registerCallbacks)
        {
            Guard.NotNull(registerCallbacks, nameof(registerCallbacks));
            this.registerCallbacks = registerCallbacks;
        }

        public void Initialize()
        {
            AssemblyScanner
                .ScanForAssemblies(Conventions.AssemblySearchPattern + ".exe")
                .ScanForAssemblies(Conventions.AssemblySearchPattern + ".dll")
                .ScanForTypes()
                .RegisterMenu(this.registerCallbacks)
                .RegisterStatusBar(this.registerCallbacks)
                .RegisterDialogs(this.registerCallbacks)
                .RegisterDocuments(this.registerCallbacks)
                .RegisterViewModels(this.registerCallbacks);
        }
    }
}