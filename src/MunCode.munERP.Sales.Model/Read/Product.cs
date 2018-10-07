namespace MunCode.munERP.Sales.Model.Read
{
    using MunCode.Core.Design.Domain;

    public class Product
    {
        public Product(int id, string name, Money price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        protected Product()
        {
        }

        public int Id { get; protected set; }

        public string Name { get; protected set; }

        public Money Price { get; protected set; }
    }
}