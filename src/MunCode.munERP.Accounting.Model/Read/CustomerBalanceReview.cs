namespace MunCode.munERP.Accounting.Model.Read
{
    using MunCode.Core.Design.Domain;

    public class CustomerBalanceReview
    {
        public CustomerBalanceReview(int id, string customerName, Money creditTotal, Money receivableTotal)
        {
            this.Id = id;
            this.CustomerName = customerName;
            this.CreditTotal = creditTotal;
            this.ReceivableTotal = receivableTotal;
        }

        protected CustomerBalanceReview()
        {
        }

        public int Id { get; protected set; }

        public string CustomerName { get; protected set; }

        public Money CreditTotal { get; protected set; }

        public Money ReceivableTotal { get; protected set; }
    }
}
