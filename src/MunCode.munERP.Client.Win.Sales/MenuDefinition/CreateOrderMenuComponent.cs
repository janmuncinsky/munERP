namespace MunCode.munERP.Client.Win.Sales.MenuDefinition
{
    using Caliburn.Micro;

    using MunCode.Core.Design.Patterns.Gof.Composite;
    using MunCode.Core.Ioc;
    using MunCode.Core.Wpf.DialogService;
    using MunCode.Core.Wpf.DocumentModel.Menu;
    using MunCode.munERP.Client.Win.Sales.Resources;
    using MunCode.munERP.Client.Win.Sales.UI.CreateOrder;

    [Parent(typeof(SalesMenuComposite))]
    public class CreateOrderMenuComponent : MenuComponent<CreateOrderViewModel>
    {
        public CreateOrderMenuComponent(
            IEventAggregator eventAggregator,
            IWindowFactory windowFactory,
            ICallScopeFactory callScopeFactory)
            : base(eventAggregator, windowFactory, callScopeFactory)
        {
        }

        public override string Header => Translation.menuCreateOrder;
    }
}