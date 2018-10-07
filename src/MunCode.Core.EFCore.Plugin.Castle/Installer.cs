namespace MunCode.Core.EFCore.Plugin.Castle
{
    using global::Castle.Facilities.TypedFactory;
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using Microsoft.Extensions.Options;

    using MunCode.Core.AppHosting;
    using MunCode.Core.Data;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDbContextFactory>().AsFactory(),
                Component.For<IConfigureOptions<DatabaseConfig>>().ImplementedBy<ConfigurationBinder<DatabaseConfig>>().LifestyleSingleton(),
                Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(EFRepository<,>)).LifestyleScoped(),
                Component.For<IUnitOfWork>().ImplementedBy<EFUnitOfWork>().LifestyleScoped(),
                Component.For<IAppInitializer>().ImplementedBy<DatabaseAppInitializer>().LifestyleSingleton());
        }
    }
}
