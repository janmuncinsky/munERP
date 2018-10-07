namespace MunCode.Core.AppHosting
{
    using global::EasyNetQ.Logging;

    using MunCode.Core.Guards;

    public class EasyNetQAppInitializer : IAppInitializer
    {
        private readonly ILogProvider logProvider;

        public EasyNetQAppInitializer(ILogProvider logProvider)
        {
            Guard.NotNull(logProvider, nameof(logProvider));
            this.logProvider = logProvider;
        }

        public void Initialize()
        {
            LogProvider.SetCurrentLogProvider(this.logProvider);
        }
    }
}