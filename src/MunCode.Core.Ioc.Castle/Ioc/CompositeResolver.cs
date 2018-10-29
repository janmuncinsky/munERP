namespace MunCode.Core.Ioc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using global::Castle.Core;
    using global::Castle.MicroKernel;
    using global::Castle.MicroKernel.Context;
    using global::Castle.MicroKernel.Resolvers.SpecializedResolvers;

    using MunCode.Core.Design.Patterns.Gof.Composite;
    using MunCode.Core.Guards;

    public class CompositeResolver : CollectionResolver
    {
        public CompositeResolver(IKernel kernel, bool allowEmptyCollections = false)
            : base(kernel, allowEmptyCollections)
        {
            Guard.NotNull(kernel, nameof(kernel));
        }

        public override object Resolve(
            CreationContext context,
            ISubDependencyResolver contextHandlerResolver,
            ComponentModel model,
            DependencyModel dependency)
        {
            var collection = (Array)base.Resolve(context, contextHandlerResolver, model, dependency);

            var children = new List<object>();
            var isComponent = Attribute.GetCustomAttribute(model.Implementation, typeof(ComponentAttribute)) is ComponentAttribute;

            foreach (var o in collection)
            {
                var component = (ComponentAttribute)Attribute.GetCustomAttribute(o.GetType(), typeof(ComponentAttribute));
                if ((!isComponent && (component == null || component.IsRoot)) ||
                    (component != null && component.IsChildOf(model.Implementation)))
                {
                    children.Add(o);
                }
            }

            var instance = Array.CreateInstance(this.GetItemType(dependency.TargetItemType), children.Count);
            ((ICollection)children).CopyTo(instance, 0);

            return instance;
        }
    }
}