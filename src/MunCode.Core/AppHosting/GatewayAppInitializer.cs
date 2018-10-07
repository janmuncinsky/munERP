namespace MunCode.Core.AppHosting
{
    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;
    using MunCode.Core.Reflection.Scanning;

    public class GatewayAppInitializer : IAppInitializer
    {
        private const string MessageAssemblySearchPattern = "*.Model.dll";
        private readonly IRegisterCallbacks registerCallbacks;

        public GatewayAppInitializer(IRegisterCallbacks registerCallbacks)
        {
            Guard.NotNull(registerCallbacks, nameof(registerCallbacks));
            this.registerCallbacks = registerCallbacks;
        }

        public void Initialize()
        {
            AssemblyScanner.ScanForAssemblies(MessageAssemblySearchPattern)
                .ScanForMessages()
                .RedirectMessages(this.registerCallbacks);
        }
    }
}