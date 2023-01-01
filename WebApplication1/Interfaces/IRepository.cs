namespace WebApplication1.Interfaces;

public interface IRepository<T> where T : class, IPrimaryKey, new()
{
    Task<IEnumerable<T>> SelectAsync(CancellationToken cancellationToken = default);
    Task<T?> SelectAsync(int id, CancellationToken cancellationToken = default);
    Task<int?> InsertAsync(T? model, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(T? model, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
