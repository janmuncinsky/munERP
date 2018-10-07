namespace MunCode.munERP.Sales.Model.Read
{
    public class Customer
    {
        public Customer(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        protected Customer()
        {
        }

        public int Id { get; protected set; }

        public string Name { get; protected set; }
    }
}