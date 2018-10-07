namespace MunCode.Core.AppHosting
{
    using System.Threading;

    using MunCode.Core.Guards;
    using MunCode.Core.Ioc;

    public class DatabaseAppInitializer : IAppInitializer
    {
        private readonly IDbContextFactory contextFactory;
        private readonly ICallScopeFactory callScopeFactory;

        public DatabaseAppInitializer(IDbContextFactory contextFactory, ICallScopeFactory callScopeFactory)
        {
            Guard.NotNull(callScopeFactory, nameof(callScopeFactory));
            Guard.NotNull(contextFactory, nameof(contextFactory));
            this.contextFactory = contextFactory;
            this.callScopeFactory = callScopeFactory;
        }

        public void Initialize()
        {
            // todo add retry mechanism for cases when sql container is not ready
            Thread.Sleep(5000);
            using (this.callScopeFactory.CreateScope())
            {
                var context = this.contextFactory.Create();
                context.Database.EnsureCreated();
            }
        }
    }
}