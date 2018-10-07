namespace MunCode.Core.Ioc.Castle.Tests
{
    using global::Castle.MicroKernel.Registration;
    using global::Castle.Windsor;

    using MunCode.Core.Ioc.Castle.Tests.Fakes;

    using NUnit.Framework;

    //using MunCode.Core.Castle.Facilities.GenericFacility;

    [TestFixture]
    public class GenericFacilityTests
    {
        [Test]
        public void DecoratorsAreResolvedInRightOrder()
        {
            var container = new WindsorContainer();
            //container.AddFacility<GenericFacility>();

            container.Register(Component.For(typeof(IFake<>)).ImplementedBy(typeof(FakeDecorator<>)));
            container.Register(Component.For<IFake<object>>().ImplementedBy<Fake>());

            Assert.That(container.Resolve<IFake<object>>(), Is.TypeOf<FakeDecorator<object>>());
        }
    }
}