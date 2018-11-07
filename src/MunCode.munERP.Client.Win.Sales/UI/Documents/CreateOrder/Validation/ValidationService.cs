namespace MunCode.munERP.Client.Win.Sales.UI.Documents.CreateOrder.Validation
{
    using System.Text;

    using FluentValidation;

    using MunCode.Core.Guards;
    using MunCode.Core.Wpf.DialogService;

    public class ValidationService<T> : IValidationService<T>
    {
        private readonly IValidator<T> validator;

        private readonly IMessageBoxService messageBoxService;

        public ValidationService(IValidator<T> validator, IMessageBoxService messageBoxService)
        {
            Guard.NotNull(validator, nameof(validator));
            Guard.NotNull(messageBoxService, nameof(messageBoxService));

            this.validator = validator;
            this.messageBoxService = messageBoxService;
        }

        public bool IsValid(T model)
        {
            var result = this.validator.Validate(model);

            if (!result.IsValid)
            {
                var builder = new StringBuilder();

                foreach (var validationFailure in result.Errors)
                {
                    builder.AppendLine(validationFailure.ErrorMessage);
                }

                this.messageBoxService.ShowMessageBox(builder.ToString());
            }

            return result.IsValid;
        }
    }
}