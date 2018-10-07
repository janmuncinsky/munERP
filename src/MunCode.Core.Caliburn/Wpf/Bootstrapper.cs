namespace MunCode.Core.Wpf
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Threading;

    using Caliburn.Micro;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    using MunCode.Core.Ioc;
    using MunCode.Core.Logging;
    using MunCode.Core.Reflection.Scanning;

    public abstract class Bootstrapper<TShellViewModel> : BootstrapperBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<TShellViewModel> logger;

        protected Bootstrapper()
        {
            this.serviceProvider = new HostBuilder()
                .ConfigureAppConfiguration(config => config.AddJsonFile("Config.json"))
                .UseServiceProviderFactory(new AppBuilderServiceProviderFactory())
                .Build()
                .Services;
            this.logger = this.serviceProvider.GetService<ILogger<TShellViewModel>>();
            this.Initialize();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return this.serviceProvider.GetService(serviceType);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.serviceProvider.GetServices(service);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.logger.LogAppStartUp();
            this.DisplayRootViewFor<TShellViewModel>();
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = AssemblyScanner
                .ScanForAssemblies(Conventions.AssemblySearchPattern + ".exe")
                .ScanForAssemblies(Conventions.AssemblySearchPattern + ".dll")
                .Select(r => r.Assembly);
            return base.SelectAssemblies().Union(assemblies);
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            this.logger.LogUnhandledException(e.Exception);
            e.Handled = false;
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            this.logger.LogAppTermination();
            base.OnExit(sender, e);
        }
    }
}
