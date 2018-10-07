namespace MunCode.munERP.Sales.Domain.Maps.Read
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using MunCode.Core.Data.Maps;
    using MunCode.munERP.Sales.Model.Read;

    public class CustomerMap : ReadModelEntityMap<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            builder.Property(i => i.Name).IsRequired();
            builder.HasData(new Customer(1, "Jan Muncinsky"), new Customer(2, "Ondrej Muncinsky"));
        }
    }
}