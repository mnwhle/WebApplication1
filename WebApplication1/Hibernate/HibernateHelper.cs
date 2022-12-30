namespace WebApplication1.Hibernate;

public class HibernateHelper
{
    private static ISessionFactory _sessionFactory = null!;
    private static ISessionFactory SessionFactory
    {
        get
        {
            if (_sessionFactory is null)
            {
                var cfg = new Configuration();
                cfg.Configure();
                cfg.AddAssembly(typeof(HibernateHelper).Assembly);
                _sessionFactory = cfg.BuildSessionFactory();
            }
            return _sessionFactory;
        }
    }

    public static NHibernate.ISession OpenSession() => SessionFactory.OpenSession();

    public static void LoadNHibernateCfg()
    {
        var cfg = new Configuration();
        cfg.Configure();
        cfg.AddAssembly(typeof(Product).Assembly);
        new SchemaExport(cfg).Execute(true, true, false);
    }
}
