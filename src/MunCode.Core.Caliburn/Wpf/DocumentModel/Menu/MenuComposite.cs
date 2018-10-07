namespace MunCode.Core.Wpf.DocumentModel.Menu
{
    using System.Collections.Generic;

    using MunCode.Core.Guards;

    public abstract class MenuComposite : IMenuComponent
    {
        protected MenuComposite(ICollection<IMenuComponent> children)
        {
            Guard.NotNull(children, nameof(children));
            this.Children = children;
        }

        public ICollection<IMenuComponent> Children { get; }

        public abstract string Header { get; }
    }
}