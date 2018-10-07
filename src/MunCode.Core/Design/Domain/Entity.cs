namespace MunCode.Core.Design.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MunCode.Core.Messaging.Messages;

    public abstract class Entity<T> : IEquatable<Entity<T>>, IHaveDomainEvents
    {
        private readonly List<IEvent> domainEvents = new List<IEvent>();

        protected Entity()
        {
        }

        protected Entity(T id)
        {
            this.Id = id;
        }

        public IReadOnlyList<IEvent> DomainEvents => this.domainEvents.ToList();

        public T Id { get; protected set; }

        protected byte[] Version { get; set; }

        public void ClearDomainEvents()
        {
            this.domainEvents.Clear();
        }

        public bool Equals(Entity<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (other.GetType() != this.GetType())
            {
                return false;
            }

            return EqualityComparer<T>.Default.Equals(this.Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            return this.Equals((Entity<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(this.Id);
        }

        protected void RaiseEvent(IEvent @event)
        {
            this.domainEvents.Add(@event);
        }
    }
}