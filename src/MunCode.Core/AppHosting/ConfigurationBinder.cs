namespace MunCode.Core.AppHosting
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    using MunCode.Core.Guards;

    public class ConfigurationBinder<TConfig> : IConfigureOptions<TConfig>
        where TConfig : class
    {
        private readonly IConfiguration configuration;

        public ConfigurationBinder(IConfiguration configuration)
        {
            Guard.NotNull(configuration, nameof(configuration));
            this.configuration = configuration;
        }

        public virtual void Configure(TConfig config)
        {
            Guard.NotNull(config, nameof(config));
            this.configuration.GetSection(config.GetType().Name).Bind(config);
        }
    }
}