namespace WebApplication1.Config;

public class Options : ConfigBase
{
    protected override string SectionKey { get; } = nameof(Options);

    public Options(ILogger<Options> logger, IConfiguration config) : base(logger, config)
    {
        ConnectionString = new(this, nameof(ConnectionString), () => string.Empty);
    }

    public ConfigOption<string> ConnectionString { get; private set; }
}
