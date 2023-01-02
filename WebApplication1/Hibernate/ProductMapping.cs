namespace WebApplication1.Hibernate;

public class ProductMapping : ClassMap<Product>
{
    public ProductMapping()
    {
        Id(x => x.Id).GeneratedBy.Identity();
        Map(x => x.Name).Length(60).Not.Nullable();
    }
}
