namespace MunCode.Core.Design.Patterns.Gof.Composite
{
    using System;

    using MunCode.Core.Guards;

    [AttributeUsage(AttributeTargets.Class)]
    public class ParentAttribute : Attribute
    {
        public ParentAttribute(Type parentType)
        {
            Guard.NotNull(parentType, nameof(parentType));
            this.ParentType = parentType;
        }

        public Type ParentType { get; }
    }
}