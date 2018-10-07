namespace MunCode.Core.AppHosting
{
    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;
    using MunCode.Core.Reflection.Scanning;

    public class ApiAppInitializer : IAppInitializer
    {
        private const string MessageHandlerSearchPattern = "*.Api.dll";
        private readonly IRegisterCallbacks registerCallbacks;

        public ApiAppInitializer(IRegisterCallbacks registerCallbacks)
        {
            Guard.NotNull(registerCallbacks, nameof(registerCallbacks));
            this.registerCallbacks = registerCallbacks;
        }

        public void Initialize()
        {
            AssemblyScanner.ScanForAssemblies(MessageHandlerSearchPattern)
                .ScanForMessageConsumers()
                .SubscribeMessages(this.registerCallbacks);
        }
    }
}