namespace WebApplication1.Hibernate;

public class ProductMapping : ClassMap<Product>
{
    public ProductMapping()
    {
        Table(nameof(Product).TableName());
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Name).Length(60).Not.Nullable().Unique().Index(nameof(Product.Name).IndexName());
    }
}
