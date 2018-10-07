namespace MunCode.Core.Ioc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Castle.Windsor.Installer;

    using MunCode.Core.Guards;

    internal class ExtendedInstallerFactory : InstallerFactory
    {
        private readonly string commonInstallerName;

        public ExtendedInstallerFactory(string commonInstallerName)
        {
            Guard.NotNull(commonInstallerName, nameof(commonInstallerName));
            this.commonInstallerName = commonInstallerName;
        }

        public override IEnumerable<Type> Select(IEnumerable<Type> installerTypes)
        {
            var enumerable = installerTypes as Type[] ?? installerTypes.ToArray();
            var commonInstallers = enumerable.Where(t => t.Name.Contains(this.commonInstallerName));
            var installers = commonInstallers as Type[] ?? commonInstallers.ToArray();

            foreach (var installer in installers)
            {
                yield return installer;
            }

            foreach (var installer in enumerable.Except(installers))
            {
                yield return installer;
            }
        }
    }
}