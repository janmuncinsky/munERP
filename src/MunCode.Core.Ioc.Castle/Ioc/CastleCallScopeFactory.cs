namespace MunCode.Core.Ioc
{
    using System;

    using global::Castle.MicroKernel;
    using global::Castle.MicroKernel.Lifestyle;

    using MunCode.Core.Guards;

    public class CastleCallScopeFactory : ICallScopeFactory
    {
        private readonly IKernel kernel;

        public CastleCallScopeFactory(IKernel kernel)
        {
            Guard.NotNull(kernel, nameof(kernel));
            this.kernel = kernel;
        }

        public IDisposable CreateScope()
        {
            return this.kernel.BeginScope() ?? new EmptyDisposable();
        }

        private class EmptyDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}