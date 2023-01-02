namespace WebApplication1.Config;

public abstract class ConfigBase
{
    protected virtual string SectionKey { get; } = string.Empty;
    protected string SectionPrefix => string.IsNullOrEmpty(SectionKey) ? SectionKey : SectionKey + ":";

    protected readonly ILogger log;
    protected readonly IConfiguration cfg;

    public static readonly string LogDir = "LogFiles";
    public static readonly string LogFileExtension = "log";

    public ConfigBase(ILogger logger, IConfiguration config)
    {
        log = logger;
        cfg = config;
    }

    public T GetValue<T>(string key, Func<T> fnDefaultValue, Func<T, T>? fnOnReloadAdjust)
    {
        string complete_key = SectionPrefix + key;
        T value = cfg.GetValue(complete_key, fnDefaultValue()) ?? fnDefaultValue();
        log.LogInformation($"Option loaded: {complete_key} = {value}");
        if (fnOnReloadAdjust is not null)
        {
            value = fnOnReloadAdjust(value);
            log.LogInformation($"Option adjusted: {complete_key} = {value}");
        }
        return value;
    }

    //public HashSet<T2> GetArray<T1, T2>(string key, Func<List<T1>, HashSet<T2>> fnOnReloadAdjust)
    //{
    //    string complete_key = SectionPrefix + key;
    //    List<T1> array = cfg.GetSection(complete_key).Get<List<T1>>() ?? new();
    //    log.Info($"Option loaded: {complete_key} = {array.FormatCollectionArray(20)}");
    //    HashSet<T2> hashset = fnOnReloadAdjust(array);
    //    log.Info($"Option adjusted: {complete_key} = {hashset.FormatCollectionHash(20)}");
    //    return hashset;
    //}
}
