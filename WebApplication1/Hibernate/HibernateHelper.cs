namespace WebApplication1.Hibernate;

public class HibernateHelper
{
    public static ISessionFactory SessionFactory { get; private set; } = null!;

    public static NHibernate.ISession OpenSession() => SessionFactory.OpenSession();

    private static ISessionFactory InitSessionFactoryFluent(Options options)
    {
        return Fluently.Configure()
            .Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString(options.ConnectionString).ShowSql().FormatSql())
            .Cache(
                c => c.UseQueryCache()
                    .UseSecondLevelCache()
                    .ProviderClass<NHibernate.Cache.HashtableCacheProvider>())
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
            .BuildSessionFactory();
    }

    private static ISessionFactory InitSessionFactoryFromXml()
    {
        var cfg = new Configuration();
        cfg.Configure();
        cfg.AddAssembly(typeof(HibernateHelper).Assembly);
        return cfg.BuildSessionFactory();
    }

    public static void InitSessionFactory(Options options, bool overwrite = false)
    {
        if ((SessionFactory is null) || overwrite)
        {
            SessionFactory = InitSessionFactoryFluent(options);
        }
    }

    public static void LoadNHibernateCfg()
    {
        var cfg = new Configuration();
        cfg.Configure();
        cfg.AddAssembly(typeof(Product).Assembly);
        new SchemaExport(cfg).Execute(true, true, false);
    }
}
