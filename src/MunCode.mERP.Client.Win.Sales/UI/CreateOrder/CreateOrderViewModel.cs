namespace MunCode.mERP.Client.Win.Sales.UI.CreateOrder
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

    public class CreateOrderViewModel : PropertyChangedBase, IDocument, ICanBeInitialized
    {
        private readonly IMessageBus bus;
        private readonly IWindowService windowService;
        private readonly ObservableCollection<OrderItemViewModel> orderItems;
        private short itemCounter;
        private Customer selectedCustomer;

        public CreateOrderViewModel(IMessageBus bus, IWindowService windowService)
        {
            Guard.NotNull(bus, nameof(bus));
            Guard.NotNull(windowService, nameof(windowService));
            this.bus = bus;
            this.windowService = windowService;
            this.orderItems = new ObservableCollection<OrderItemViewModel>();
        }

        public string DisplayName { get; set; } = Translation.menuCreateOrder;

        public IEnumerable<Customer> Customers { get; private set; }

        public IEnumerable<OrderItemViewModel> OrderItems => this.orderItems;

        public Customer SelectedCustomer
        {
            get => this.selectedCustomer;

            set
            {
                this.selectedCustomer = value;
                this.NotifyOfPropertyChange(nameof(this.CanCreateOrder));
                this.NotifyOfPropertyChange(nameof(this.selectedCustomer));
            }
        }

        public bool CanCreateOrder => this.orderItems.Any() && this.SelectedCustomer != null;

        public async void AddItem()
        {
            var product = await this.windowService.ShowDialog<Product>();
            if (product != null)
            {
                this.itemCounter++;
                var item = new OrderItemViewModel(this.itemCounter, 1, product);
                this.orderItems.Add(item);
                this.NotifyOfPropertyChange(nameof(this.CanCreateOrder));
            }
        }

        public async Task CreateOrder()
        {
            var items = this.orderItems.Select(i => new OrderItem(i.LineNumber, i.Product.Id, i.Quantity));
            var command = new CreateOrder(this.SelectedCustomer.Id, items);
            var result = await this.bus.Request<CreateOrder, OrderStatusResponse>(command);
            this.windowService.ShowMessageBox(string.Format(Translation.msgOrderStatus, result.OrderStatus));
        }

        public async Task Initialize()
        {
            this.Customers = await this.bus.Request<GetAllCustomers, Customer[]>(new GetAllCustomers());
            this.NotifyOfPropertyChange(nameof(this.Customers));
        }
    }
}