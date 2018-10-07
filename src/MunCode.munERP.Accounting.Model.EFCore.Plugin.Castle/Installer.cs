namespace MunCode.munERP.Accounting.Model.EFCore.Plugin.Castle
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using Microsoft.EntityFrameworkCore;

    using MunCode.Core.Data;
    using MunCode.munERP.Accounting.Domain;
    using MunCode.munERP.Accounting.Model.Domain;

    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IRepository<CustomerBalance, int>>().ImplementedBy<CustomerBalanceRepository>().LifestyleScoped().IsDefault(),
                Component.For<DbContext>().ImplementedBy<AccountingContext>().LifestyleScoped());
        }
    }
}