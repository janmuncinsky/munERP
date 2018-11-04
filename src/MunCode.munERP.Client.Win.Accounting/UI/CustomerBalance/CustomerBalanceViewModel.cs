namespace MunCode.munERP.Client.Win.Accounting.UI.CustomerBalance
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Caliburn.Micro;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging.Endpoints.Output;
    using MunCode.Core.Wpf.DocumentModel;
    using MunCode.munERP.Client.Win.Accounting.Model.Messages.Requests;
    using MunCode.munERP.Client.Win.Accounting.Model.Read;
    using MunCode.munERP.Client.Win.Accounting.Resources;

    public class CustomerBalanceReviewViewModel : PropertyChangedBase, IDocument, ICanBeInitialized
    {
        private readonly IMessageBus bus;

        public CustomerBalanceReviewViewModel(IMessageBus bus)
        {
            Guard.NotNull(bus, nameof(bus));
            this.bus = bus;
        }

        public string DisplayName { get; set; } = Translation.menuCustomerBalanceReview;

        public IEnumerable<CustomerBalanceReview> CustomerBalanceReviews { get; private set; }

        public async Task Initialize()
        {
            var request = new GetAllCustomerBalanceReviews();
            this.CustomerBalanceReviews = await this.bus.Request<GetAllCustomerBalanceReviews, CustomerBalanceReview[]>(request);
            this.NotifyOfPropertyChange(nameof(this.CustomerBalanceReviews));
        }
    }
}