namespace MunCode.munERP.Client.Win.Sales.Model.Read
{
    public class Product
    {
        public Product(int id, string name, Money price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public int Id { get; }

        public string Name { get; }

        public Money Price { get; }
    }
}