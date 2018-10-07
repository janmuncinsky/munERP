namespace MunCode.Core.AppHosting
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Hosting;

    using MunCode.Core.Guards;

    public class HostedService : IHostedService
    {
        private readonly ICollection<IHostInitializer> initializers;
        private readonly ICollection<IHostTerminator> terminators;

        public HostedService(ICollection<IHostInitializer> initializers, ICollection<IHostTerminator> terminators)
        {
            Guard.NotNull(terminators, nameof(terminators));
            Guard.NotNull(initializers, nameof(initializers));
            this.initializers = initializers;
            this.terminators = terminators;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var initializer in this.initializers)
            {
                initializer.Initialize();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (var terminator in this.terminators)
            {
                terminator.Terminate();
            }

            return Task.CompletedTask;
        }
    }
}