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
        using NHibernate.ISession session = HibernateHelper.OpenSession();
        using ITransaction transaction = session.BeginTransaction();
        var result = await session.SaveAsync(model, cancellationToken);
        transaction.Commit();
        return (int?)result;
    }

    //public virtual Task<T?> UpdateAsync(T? model, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        if (model is not null)
    //        {
    //            using NHibernate.ISession session = HibernateHelper.OpenSession();
    //            using ITransaction transaction = session.BeginTransaction();
    //            session.SaveAsync(model, cancellationToken);
    //            transaction.Commit();
    //            return Task.FromResult(true);
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError(e.Message);
    //    }
    //    return Task.FromResult(false);
    //}

    //public virtual Task<T?> DeleteAsync(P id, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        using NHibernate.ISession session = HibernateHelper.OpenSession();
    //        using ITransaction transaction = session.BeginTransaction();
    //        session.DeleteAsync(id);
    //        transaction.Commit();
    //        return Task.FromResult(true);
    //    }
    //    catch (Exception e)
    //    {
    //        _logger.LogError(e.Message);
    //        return Task.FromResult(false);
    //    }
    //}
}
