namespace WebApplication1.Interfaces;

public interface IRepository<T> where T : class, IPrimaryKey, new()
{
    //TODO Conside changing the interface for managing complex transactions. Maybe through a wrapper class?
    Task<IEnumerable<T>> SelectAsync(CancellationToken cancellationToken);
    Task<T?> SelectAsync(int id, CancellationToken cancellationToken);
    Task<int?> InsertAsync(T? model, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T? model, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
