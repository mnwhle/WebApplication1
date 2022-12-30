namespace WebApplication1.Interfaces;

public interface IRepository<T, P>
    where T : class, IPrimaryKey<P>, new()
    where P : struct
{
    Task<IEnumerable<T>> SelectAsync(CancellationToken cancellationToken = default);
    Task<T?> SelectAsync(P id, CancellationToken cancellationToken = default);
    Task<T?> InsertAsync(T? model, CancellationToken cancellationToken = default);
    //Task<T?> UpdateAsync(T? model, CancellationToken cancellationToken = default);
    //Task<T?> DeleteAsync(P id, CancellationToken cancellationToken = default);
}
