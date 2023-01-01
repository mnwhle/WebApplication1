namespace WebApplication1.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> IsNameAlreadyExistsAsync(string name, CancellationToken cancellationToken = default);
}
