namespace MunCode.munERP.Client.Win.Sales.UI.Documents.CreateOrder.Validation
{
    using System.Linq;

    using FluentValidation;

    using MunCode.munERP.Client.Win.Sales.Resources;

    public class OrderValidator : AbstractValidator<CreateOrderViewModel>
    {
        public OrderValidator()
        {
            this.RuleFor(o => o.SelectedCustomer).NotNull().WithMessage(Translation.NotNull);
            this.RuleFor(o => o.OrderItems).Must(i => i.Any()).WithMessage(Translation.CollectionEmpty);
        }
    }
}