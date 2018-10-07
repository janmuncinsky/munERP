namespace MunCode.Core.AppHosting
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using MunCode.Core.Guards;

    public class Log4netAppInitializer : IAppInitializer
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly string appName;
        private readonly string hostName;

        public Log4netAppInitializer(ILoggerFactory loggerFactory, IOptions<AppConfig> config)
        {
            Guard.NotNull(config, nameof(config));
            Guard.NotNull(loggerFactory, nameof(loggerFactory));
            this.loggerFactory = loggerFactory;
            this.appName = config.Value.AppName;
            this.hostName = config.Value.HostName;
        }

        public void Initialize()
        {
            log4net.GlobalContext.Properties["LogName"] = $"{this.appName}-{this.hostName}";
            this.loggerFactory.AddLog4Net();
        }
    }
}