namespace MunCode.munERP.Client.Win.Accounting.MenuDefinition
{
    using System.Collections.Generic;

    using MunCode.Core.Design.Patterns.Gof.Composite;
    using MunCode.Core.Wpf.DocumentModel.Menu;
    using MunCode.munERP.Client.Win.Accounting.Resources;

    [Component]
    public class AccountingMenuComposite : MenuComposite
    {
        public AccountingMenuComposite(ICollection<IMenuComponent> children)
            : base(children)
        {
        }

        public override string Header => Translation.menuAccounting;
    }
}