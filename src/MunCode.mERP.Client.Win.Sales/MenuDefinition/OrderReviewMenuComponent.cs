﻿namespace MunCode.mERP.Client.Win.Sales.MenuDefinition
{
    using Caliburn.Micro;

    using MunCode.Core.Design.Patterns.Gof.Composite;
    using MunCode.Core.Ioc;
    using MunCode.Core.Wpf.DialogService;
    using MunCode.Core.Wpf.DocumentModel.Menu;
    using MunCode.mERP.Client.Win.Sales.Resources;
    using MunCode.mERP.Client.Win.Sales.UI.OrderReview;

    [Parent(typeof(SalesMenuComposite))]
    public class OrderReviewMenuComponent : MenuComponent<OrderReviewViewModel>
    {
        public OrderReviewMenuComponent(
            IEventAggregator eventAggregator,
            IWindowFactory windowFactory,
            ICallScopeFactory callScopeFactory)
            : base(eventAggregator, windowFactory, callScopeFactory)
        {
        }

        public override string Header => Translation.menuOrderReview;
    }
}