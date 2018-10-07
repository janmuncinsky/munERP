namespace MunCode.munERP.Client.Win.Sales.MenuDefinition
{
    using System.Collections.Generic;

    using MunCode.Core.Wpf.DocumentModel.Menu;
    using MunCode.munERP.Client.Win.Sales.Resources;

    public class SalesMenuComposite : MenuComposite
    {
        public SalesMenuComposite(ICollection<IMenuComponent> children)
            : base(children)
        {
        }

        public override string Header => Translation.menuSales;
    }
}