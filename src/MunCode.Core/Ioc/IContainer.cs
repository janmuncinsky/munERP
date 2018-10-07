namespace MunCode.Core.Ioc
{
    using System;
    using System.Collections.Generic;

    public interface IContainer
    {
        void Install();

        object Resolve(Type serviceType);

        object Resolve(string key, Type serviceType);

        IEnumerable<object> ResolveAll(Type service);
    }
}
