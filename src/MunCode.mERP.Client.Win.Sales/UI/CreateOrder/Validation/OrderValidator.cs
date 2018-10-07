namespace MunCode.mERP.Client.Win.Sales.UI.CreateOrder.Validation
{
    using System.Linq;

    using FluentValidation;

    using MunCode.mERP.Client.Win.Sales.Resources;

    public class OrderValidator : AbstractValidator<CreateOrderViewModel>
    {
        public OrderValidator()
        {
            this.RuleFor(o => o.SelectedCustomer).NotNull().WithMessage(Translation.NotNull);
            this.RuleFor(o => o.OrderItems).Must(i => i.Any()).WithMessage(Translation.CollectionEmpty);
        }
    }
}