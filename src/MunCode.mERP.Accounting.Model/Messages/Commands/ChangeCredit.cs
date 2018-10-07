namespace MunCode.mERP.Accounting.Model.Messages.Commands
{
    using MunCode.Core.Design.Domain;
    using MunCode.Core.Messaging.Messages;

    public class ChangeCredit : ICommand
    {
        public ChangeCredit(int customerId, Money creditTotal)
        {
            this.CustomerId = customerId;
            this.CreditTotal = creditTotal;
        }

        public int CustomerId { get; }

        public Money CreditTotal { get; }
    }
}