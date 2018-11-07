namespace MunCode.munERP.Client.Win.Sales.UI.Documents.CreateOrder.Validation
{
    public interface IValidationService<in T>
    {
        bool IsValid(T model);
    }
}