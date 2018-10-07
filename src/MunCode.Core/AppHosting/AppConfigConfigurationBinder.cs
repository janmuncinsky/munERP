namespace MunCode.Core.AppHosting
{
    using System;

    using Microsoft.Extensions.Configuration;

    using MunCode.Core.Guards;

    public class AppConfigConfigurationBinder : ConfigurationBinder<AppConfig>
    {
        private readonly IConfiguration configuration;

        public AppConfigConfigurationBinder(IConfiguration configuration)
            : base(configuration)
        {
            Guard.NotNull(configuration, nameof(configuration));
            this.configuration = configuration;
        }

        public override void Configure(AppConfig config)
        {
            Guard.NotNull(config, nameof(config));
            config.HostName = this.configuration["HostName"] ?? Environment.MachineName;
            config.AppName = this.configuration["APPLICATIONKEY"];
        }
    }
}