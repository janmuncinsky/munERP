namespace MunCode.Core.AppHosting
{
    using System.Collections.Generic;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Input.Consuming;

    public class ConsumerDefinitionVisitorInitializer : IHostInitializer
    {
        private readonly ICollection<IConsumerDefinition> receiverDefinitions;
        private readonly ICollection<IConsumerDefinitionVisitor> visitors;

        public ConsumerDefinitionVisitorInitializer(ICollection<IConsumerDefinition> receiverDefinitions, ICollection<IConsumerDefinitionVisitor> visitors)
        {
            Guard.NotNull(visitors, nameof(visitors));
            Guard.NotNull(receiverDefinitions, nameof(receiverDefinitions));
            this.receiverDefinitions = receiverDefinitions;
            this.visitors = visitors;
        }

        public void Initialize()
        {
            foreach (var receiverDefinition in this.receiverDefinitions)
            {
                foreach (var visitor in this.visitors)
                {
                    receiverDefinition.Accept(visitor);
                }
            }
        }
    }
}