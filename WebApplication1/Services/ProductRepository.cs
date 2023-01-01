namespace WebApplication1.Services;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ILogger<ProductRepository> logger) : base(logger)
    {
    }

    public async Task<bool> IsNameAlreadyExistsAsync(string name, CancellationToken cancellationToken)
    {
        using NHibernate.ISession session = HibernateHelper.OpenSession();
        var count = await session.QueryOver<Product>().Where(x => x.Name == name).RowCountAsync(cancellationToken);
        return count > 0;
    }
}
