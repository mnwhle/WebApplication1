namespace WebApplication1.Services;

public abstract class RepositoryBase<T> : IRepository<T> where T : class, IPrimaryKey, new()
{
    private readonly ILogger _logger;

    public RepositoryBase(ILogger logger)
    {
        _logger = logger;
    }

    public virtual async Task<IEnumerable<T>> SelectAsync(CancellationToken cancellationToken)
    {
        using NHibernate.ISession session = HibernateHelper.OpenSession();
        return await session.QueryOver<T>().ListAsync<T>(); ;
    }

    public virtual async Task<T?> SelectAsync(int id, CancellationToken cancellationToken)
    {
        using NHibernate.ISession session = HibernateHelper.OpenSession();
        return await session.QueryOver<T>().Where(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<int?> InsertAsync(T? model, CancellationToken cancellationToken)
    {
        if (model is null)
        {
            return null;
        }
        try
        {
            using NHibernate.ISession session = HibernateHelper.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            var result = await session.SaveAsync(model, cancellationToken);
            transaction.Commit();
            return (int?)result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public virtual async Task<bool> UpdateAsync(T? model, CancellationToken cancellationToken)
    {
        if (model is null)
        {
            return false;
        }
        try
        {
            using NHibernate.ISession session = HibernateHelper.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            await session.UpdateAsync(model, cancellationToken);
            transaction.Commit();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        using NHibernate.ISession session = HibernateHelper.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        T? model = await SelectAsync(id, cancellationToken);
        if (model is null)
        {
            return false;
        }
        try
        {
            await session.DeleteAsync(model, cancellationToken);
            transaction.Commit();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
