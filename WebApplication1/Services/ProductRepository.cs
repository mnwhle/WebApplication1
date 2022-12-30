namespace WebApplication1.Services;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ILogger<ProductRepository> logger, IMediator mediatr) : base(logger, mediatr)
    {
    }
}
