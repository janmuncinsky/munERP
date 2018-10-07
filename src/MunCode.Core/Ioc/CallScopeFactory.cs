namespace MunCode.Core.Ioc
{
    using System;

    public interface ICallScopeFactory
    {
        IDisposable CreateScope();
    }
}