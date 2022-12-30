namespace WebApplication1.Services;

public abstract class RepositoryBase<T> : IRepository<T> where T : class, IPrimaryKeyGuid, new()
{
    private readonly ILogger _logger;

    private readonly IMediator _mediatr;

    public RepositoryBase(ILogger logger, IMediator mediatr)
    {
        _logger = logger;
        _mediatr = mediatr;
    }

    public virtual Task<IEnumerable<T>> SelectAsync(CancellationToken token)
    {
        List<T> output = new();
        try
        {
            using NHibernate.ISession session = HibernateHelper.OpenSession();
            //var result = session.QueryOver<T>().Where(x => x.Id == id);
            //    return Task.FromResult<T?>(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return Task.FromResult(output.AsEnumerable());
    }

    public virtual Task<T?> SelectAsync(Guid id, CancellationToken token)
    {
        try
        {
            using NHibernate.ISession session = HibernateHelper.OpenSession();
            T? result = session.QueryOver<T>().Where(x => x.Id == id).SingleOrDefault();
            return Task.FromResult<T?>(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        return Task.FromResult<T?>(null);
    }

    public virtual Task<T?> InsertAsync(T? model, CancellationToken token)
    {
        if (model is not null)
        {
            try
            {
                using NHibernate.ISession session = HibernateHelper.OpenSession();
                using ITransaction transaction = session.BeginTransaction();
                session.SaveAsync(model, token);
                transaction.Commit();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return Task.FromResult<T?>(model);
        }
        else
        {
            return Task.FromResult<T?>(model);
        }
    }

    //public virtual Task<T?> UpdateAsync(T? model, CancellationToken token)
    //{
    //    try
    //    {
    //        if (model is not null)
    //        {
    //            using NHibernate.ISession session = HibernateHelper.OpenSession();
    //            using ITransaction transaction = session.BeginTransaction();
    //            session.SaveAsync(model, token);
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

    //public virtual Task<T?> DeleteAsync(Guid id, CancellationToken token)
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
