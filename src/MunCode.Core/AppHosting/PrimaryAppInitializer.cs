namespace MunCode.Core.AppHosting
{
    using System.Collections.Generic;

    using MunCode.Core.Guards;

    public class PrimaryAppInitializer : IAppInitializer
    {
        private readonly IEnumerable<IAppInitializer> initializers;

        public PrimaryAppInitializer(ICollection<IAppInitializer> initializers)
        {
            Guard.NotNull(initializers, nameof(initializers));
            this.initializers = initializers;
        }

        public void Initialize()
        {
            foreach (var appInitializer in this.initializers)
            {
                appInitializer.Initialize();
            }
        }
    }
}