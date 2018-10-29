namespace MunCode.Core.Design.Patterns.Gof.Composite
{
    using System;

    using MunCode.Core.Guards;

    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        private readonly Type parentType;

        public ComponentAttribute()
        {
        }

        public ComponentAttribute(Type parentType)
        {
            this.parentType = parentType;
            Guard.NotNull(parentType, nameof(parentType));
        }

        public bool IsRoot => this.parentType == null;

        public bool IsChildOf(Type component)
        {
            return this.parentType == component;
        }
    }
}