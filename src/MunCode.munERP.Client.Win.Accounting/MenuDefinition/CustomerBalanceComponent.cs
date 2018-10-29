namespace MunCode.munERP.Client.Win.Accounting.MenuDefinition
{
    using Caliburn.Micro;

    using MunCode.Core.Design.Patterns.Gof.Composite;
    using MunCode.Core.Ioc;
    using MunCode.Core.Wpf.DialogService;
    using MunCode.Core.Wpf.DocumentModel.Menu;
    using MunCode.munERP.Client.Win.Accounting.Resources;
    using MunCode.munERP.Client.Win.Accounting.UI.CustomerBalance;

    [Component(typeof(AccountingMenuComposite))]
    public class CustomerBalanceReviewComponent : MenuComponent<CustomerBalanceReviewViewModel>
    {
        public CustomerBalanceReviewComponent(IEventAggregator eventAggregator, IWindowFactory windowFactory, ICallScopeFactory callScopeFactory)
            : base(eventAggregator, windowFactory, callScopeFactory)
        {
        }

        public override string Header => Translation.menuCustomerBalanceReview;
    }
}