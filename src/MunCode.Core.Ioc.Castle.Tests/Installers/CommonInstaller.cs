namespace MunCode.Core.Ioc.Castle.Tests.Installers
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    public class CommonInstaller : IWindsorInstaller
    {
        public static int Order { get; set; }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Order = OrderCounter.Order;
            OrderCounter.Order++;
        }
    }
}