namespace MunCode.Core.Log4net.Plugin.Castle
{
    using global::Castle.Core;
    using global::Castle.MicroKernel;
    using global::Castle.MicroKernel.Context;

    using Microsoft.Extensions.Logging;

    internal class LoggerResolver : ISubDependencyResolver
    {
        private readonly IKernel container;

        public LoggerResolver(IKernel container)
        {
            this.container = container;
        }

        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return dependency.TargetType == typeof(ILogger);
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            var factory = this.container.Resolve<ILoggerFactory>();
            return factory.CreateLogger(model.Implementation);
        }
    }
}
