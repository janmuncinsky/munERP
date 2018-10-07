namespace MunCode.Core.Ioc.Castle.Tests.Installers
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.MicroKernel.SubSystems.Configuration;
    using global::Castle.Windsor;

    using MunCode.Core.Ioc.Castle.Tests.Fakes;

    public class FakeInstaller : IWindsorInstaller
    {
        public static readonly string FakeClassKey = "key";

        public static int Order { get; set; }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Fake>().Named(FakeClassKey));
            container.Register(Component.For<Fake>().Named("anotherOne"));

            Order = OrderCounter.Order;
            OrderCounter.Order++;
        }
    }
}
