namespace MunCode.Core.Messaging.Endpoints.Input
{
    using System.Threading;

    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.Extensions.Primitives;

    using MunCode.Core.Guards;

    public class InputMessageChannelProvider : IActionDescriptorChangeProvider
    {
        private readonly ApplicationPartManager applicationPartManager;
        private readonly InputMessageChannelApplicationPart part;

        public InputMessageChannelProvider(ApplicationPartManager applicationPartManager, InputMessageChannelApplicationPart part)
        {
            Guard.NotNull(part, nameof(part));
            Guard.NotNull(applicationPartManager, nameof(applicationPartManager));
            this.applicationPartManager = applicationPartManager;
            this.part = part;
        }

        public IChangeToken GetChangeToken()
        {
            this.applicationPartManager.ApplicationParts.Add(this.part);
            var tokenSource = new CancellationTokenSource();
            return new CancellationChangeToken(tokenSource.Token);
        }
    }
}