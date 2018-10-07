namespace MunCode.Core.Ioc
{
    using System;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using MunCode.Core.AppHosting;
    using MunCode.Core.Guards;

    public class AppBuilderServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
    {
        private readonly ServiceProviderInitializer serviceProviderInitializer;

        public AppBuilderServiceProviderFactory()
        {
            this.serviceProviderInitializer = new ServiceProviderInitializer();
        }

        public IServiceCollection CreateBuilder(IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));
            services.AddSingleton<IHostedService, HostedService>();
            return services;
        }

        public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
        {
            Guard.NotNull(containerBuilder, nameof(containerBuilder));
            return this.serviceProviderInitializer.Initialize(containerBuilder);
        }
    }
}