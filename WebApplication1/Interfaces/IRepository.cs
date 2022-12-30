namespace WebApplication1.Interfaces;

public interface IRepository<T> where T : class, IPrimaryKeyGuid, new()
{
    Task<IEnumerable<T>> SelectAsync(CancellationToken cancellationToken = default);
    Task<T?> SelectAsync(Guid id, CancellationToken cancellationToken = default);
    Task<T?> InsertAsync(T? model, CancellationToken cancellationToken = default);
    //Task<T?> UpdateAsync(T? model, CancellationToken cancellationToken = default);
    //Task<T?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
