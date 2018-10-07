namespace MunCode.Core.Ioc
{
    using System;
    using System.Composition;

    using global::Castle.Windsor.MsDependencyInjection;

    using Microsoft.Extensions.DependencyInjection;

    using MunCode.Core.Guards;

    [Export(typeof(IServiceProviderFactory))]
    public class WindsorRegistrationHelperAdapter : IServiceProviderFactory
    {
        public IServiceProvider Create(IServiceCollection services)
        {
            Guard.NotNull(services, nameof(services));
            var adapter = new WindsorContainerAdapter();
            adapter.Install();
            return WindsorRegistrationHelper.CreateServiceProvider(adapter.Container, services);
        }
    }
}
