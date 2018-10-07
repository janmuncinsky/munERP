namespace MunCode.mERP.Client.Win.Sales.UI.CreateOrder
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
    using MunCode.mERP.Client.Win.Sales.Model.Read;
    using MunCode.mERP.Client.Win.Sales.Resources;

    public class ProductDialogViewModel : Screen, IHaveDialogResult<Product>, ICanBeInitialized
    {
        private readonly IMessageBus bus;
        private Product selectedProduct;

        public ProductDialogViewModel(IMessageBus bus)
        {
            Guard.NotNull(bus, nameof(bus));
            this.bus = bus;
        }

        public override string DisplayName { get; set; } = Translation.AddItem;

        public IEnumerable<Product> Products { get; private set; }

        public Product SelectedProduct
        {
            get => this.selectedProduct;
            set
            {
                this.selectedProduct = value;
                this.NotifyOfPropertyChange(nameof(this.SelectedProduct));
                this.NotifyOfPropertyChange(nameof(this.CanConfirm));
            }
        }

        public Product DialogResult => this.SelectedProduct;

        public bool CanConfirm => this.SelectedProduct != null;

        public void Confirm()
        {
            this.TryClose(true);
        }

        public void Cancel()
        {
            this.TryClose(false);
        }

        public async Task Initialize()
        {
            this.Products = await this.bus.Request<GetAllProducts, Product[]>(new GetAllProducts());
            this.SelectedProduct = this.Products.FirstOrDefault();
            this.NotifyOfPropertyChange(nameof(this.Products));
        }
    }
}