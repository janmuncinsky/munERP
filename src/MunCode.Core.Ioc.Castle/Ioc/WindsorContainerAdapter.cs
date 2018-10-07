namespace MunCode.Core.Ioc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Castle.Facilities.TypedFactory;
    using Castle.MicroKernel;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    using MunCode.Core.Guards;
    using MunCode.Core.Reflection.Scanning;

    public sealed class WindsorContainerAdapter : IContainer, IDisposable
    {
        private const string InstallAssemblySuffix = "Plugin.Castle";
        private const string CommonInstallerName = "CommonInstaller";

        public IWindsorContainer Container { get; } = new WindsorContainer();

        public void Install()
        {
            this.RegisterCommonServices();

            var suffix = $"*.{InstallAssemblySuffix}.dll";
            var assemblies = AssemblyScanner
                .ScanForAssemblies(suffix)
                .Select(a => a.Assembly)
                .ToList();

            foreach (var assembly in assemblies)
            {
                this.Container.Install(FromAssembly.Instance(assembly, new ExtendedInstallerFactory(CommonInstallerName)));
            }
        }

        public object Resolve(Type serviceType)
        {
            Guard.NotNull(serviceType, nameof(serviceType));
            return this.Container.Resolve(serviceType);
        }

        public object Resolve(string key, Type serviceType)
        {
            Guard.NotNull(key, nameof(key));
            Guard.NotNull(serviceType, nameof(serviceType));
            return this.Container.Resolve(key, serviceType);
        }

        public IEnumerable<object> ResolveAll(Type service)
        {
            Guard.NotNull(service, nameof(service));
            return this.Container.ResolveAll(service).OfType<object>();
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }

        private void RegisterCommonServices()
        {
            this.Container.AddFacility<TypedFactoryFacility>();
            this.Container.Kernel.Resolver.AddSubResolver(new CompositeResolver(this.Container.Kernel, true));
            this.Container.Register(
                Component.For<IKernel>().Instance(this.Container.Kernel).LifestyleSingleton(),
                Component.For<IRegisterCallbacks>().ImplementedBy<CastleRegisterRegisterCallbacks>().LifestyleSingleton(),
                Component.For<ICallScopeFactory>().ImplementedBy<CastleCallScopeFactory>().LifestyleSingleton());
        }
    }
}
