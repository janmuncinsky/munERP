namespace MunCode.Core.Ioc.Castle.Tests
{
    using System.Linq;

    using MunCode.Core.Ioc;
    using MunCode.Core.Ioc.Castle.Tests.Fakes;
    using MunCode.Core.Ioc.Castle.Tests.Installers;

    using NUnit.Framework;

    [TestFixture]
    public class ContainerWrapperTests
    {
        private WindsorContainerAdapter container;

        [OneTimeSetUp]
        public void SetUp()
        {
            this.container = new WindsorContainerAdapter();
            //this.container.Install("MunCode.Core.Castle.Tests");
        }

        [Test]
        public void CanResolveByType()
        {
            var result = this.container.Resolve(typeof(Fake));

            Assert.That(result, Is.Not.Null.And.TypeOf<Fake>());
        }

        [Test]
        public void CanResolveByTypeAndKey()
        {
            var result = this.container.Resolve(FakeInstaller.FakeClassKey, typeof(Fake));

            Assert.That(result, Is.Not.Null.And.TypeOf<Fake>());
        }

        [Test]
        public void CanResolveAllTypes()
        {
            var result = this.container.ResolveAll(typeof(Fake)).ToList();

            Assert.That(result, Has.Count.EqualTo(2).And.All.TypeOf<Fake>());
        }

        [Test]
        public void InstallersAreInstalledInRightOrder()
        {
            Assert.That(CommonInstaller.Order, Is.EqualTo(0));
            Assert.That(FakeInstaller.Order, Is.EqualTo(1));
        }

        [Test]
        public void ExceptionIsRaisedWhenInstallAssemblyNotFound()
        {
            this.container = new WindsorContainerAdapter();

            //Assert.That(() => this.container.Install("FakeAssembly"), Throws.TypeOf<InstallationAssembliesNotFoundException>());
        }
    }
}
