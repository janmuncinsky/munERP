namespace MunCode.mERP.Client.Win.Sales.UI.CreateOrder.Validation
{
    public interface IValidationService<in T>
    {
        bool IsValid(T model);
    }
}