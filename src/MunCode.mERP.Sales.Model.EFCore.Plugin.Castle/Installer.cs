namespace MunCode.mERP.Sales.Model.EFCore.Plugin.Castle
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using Microsoft.EntityFrameworkCore;

    using MunCode.mERP.Sales.Domain;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<DbContext>().ImplementedBy<SalesContext>().LifestyleScoped());
        }
    }
}