namespace MunCode.munERP.Client.Win.Sales.Model
{
    public class Money
    {
        public Money(decimal amount, string currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public decimal Amount { get; }

        public string Currency { get; }
    }
}