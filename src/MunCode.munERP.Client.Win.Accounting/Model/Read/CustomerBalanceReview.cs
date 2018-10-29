namespace MunCode.munERP.Client.Win.Accounting.Model.Read
{
    using MunCode.Core.Design.Read;

    public class CustomerBalanceReview
    {
        public CustomerBalanceReview(string customerName, Money creditTotal, Money receivableTotal)
        {
            this.CustomerName = customerName;
            this.CreditTotal = creditTotal;
            this.ReceivableTotal = receivableTotal;
        }

        public string CustomerName { get; }

        public Money CreditTotal { get; }

        public Money ReceivableTotal { get; }
    }
}