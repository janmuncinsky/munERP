namespace MunCode.Core.Design.Domain
{
    using System.Collections.Generic;

    using MunCode.Core.Messaging.Messages;

    public interface IHaveDomainEvents
    {
        IReadOnlyList<IEvent> DomainEvents { get; }

        void ClearDomainEvents();
    }
}