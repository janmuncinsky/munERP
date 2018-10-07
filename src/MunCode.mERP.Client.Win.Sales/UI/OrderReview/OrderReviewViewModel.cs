namespace MunCode.mERP.Client.Win.Sales.UI.OrderReview
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Caliburn.Micro;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Wpf.DialogService;
    using MunCode.Core.Wpf.DocumentModel;
    using MunCode.mERP.Client.Win.Sales.Model.Messages.Requests;
    using MunCode.mERP.Client.Win.Sales.Model.Messages.Transactions;
    using MunCode.mERP.Client.Win.Sales.Model.Read;
    using MunCode.mERP.Client.Win.Sales.Resources;

    public class OrderReviewViewModel : PropertyChangedBase, IDocument, ICanBeInitialized
    {
        private readonly IMessageBus bus;
        private readonly IWindowService windowService;
        private OrderReview selectedOrder;

        public OrderReviewViewModel(IMessageBus bus, IWindowService windowService)
        {
            Guard.NotNull(windowService, nameof(windowService));
            Guard.NotNull(bus, nameof(bus));
            this.bus = bus;
            this.windowService = windowService;
        }

        public string DisplayName { get; set; } = Translation.menuOrderReview;

        public IEnumerable<OrderReview> OrderReviews { get; private set; }

        public OrderReview SelectedOrder
        {
            get => this.selectedOrder;
            set
            {
                this.selectedOrder = value;
                this.NotifyOfPropertyChange(nameof(this.SelectedOrder));
                this.NotifyOfPropertyChange(nameof(this.CanEditOrder));
            }
        }

        public bool CanEditOrder => this.SelectedOrder != null;

        public async Task Initialize()
        {
            this.OrderReviews = await this.bus.Request<GetAllOrderReviews, OrderReview[]>(new GetAllOrderReviews());
            this.SelectedOrder = this.OrderReviews.FirstOrDefault();
            this.NotifyOfPropertyChange(nameof(this.OrderReviews));
        }

        public async void AddItem()
        {
            var product = await this.windowService.ShowDialog<Product>();
            if (product != null)
            {
                var item = new OrderItem(0, product.Id, 1);
                var request = new AddOrderItem(this.SelectedOrder.Id, item);
                var result = await this.bus.Request<AddOrderItem, OrderStatusResponse>(request);
                this.windowService.ShowMessageBox(string.Format(Translation.msgOrderStatus, result.OrderStatus));
            }
        }
    }
}