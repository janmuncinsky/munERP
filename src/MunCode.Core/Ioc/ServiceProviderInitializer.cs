namespace MunCode.Core.Ioc
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.DependencyInjection;

    using MunCode.Core.AppHosting;
    using MunCode.Core.Guards;

    public class ServiceProviderInitializer
    {
        public IServiceProvider Initialize(IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));
            var serviceProviderFactory = ComponentComposer.Compose<IServiceProviderFactory>("MunCode.Core.Ioc.*.dll");
            var serviceProvider = serviceProviderFactory.Create(services);
            var initializer = serviceProvider.GetService<IAppInitializer>();
            Guard.NotNull(initializer, new InvalidOperationException("Cannot find any AppInitializer."));
            initializer.Initialize();
            return serviceProvider;
        }

        public IServiceProvider Initialize()
        {
            return this.Initialize(new EmptyServiceCollection());
        }

        private class EmptyServiceCollection : List<ServiceDescriptor>, IServiceCollection
        {
        }
    }
}