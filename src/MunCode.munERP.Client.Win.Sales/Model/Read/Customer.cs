namespace MunCode.munERP.Client.Win.Sales.Model.Read
{
    public class Customer
    {
        public Customer(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; }

        public string Name { get; }
    }
}