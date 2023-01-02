namespace WebApplication1.Config;

public class ConfigOption<T>
{
    private readonly ConfigBase _Config;

    private readonly string _Key;
    private readonly Func<T> _FnDefaultValue;
    private readonly Func<T, T>? _FnOnReloadAdjust;

    public T Value { get; private set; } = default!;

    public ConfigOption(ConfigBase config, string option, Func<T> fnDefaultValue, Func<T, T>? fnOnReloadAdjust = null)
    {
        _Config = config;
        _Key = option;
        _FnDefaultValue = fnDefaultValue;
        _FnOnReloadAdjust = fnOnReloadAdjust;
        Reload();
    }

    public virtual void Reload()
    {
        Value = _Config.GetValue(_Key, _FnDefaultValue, _FnOnReloadAdjust);
    }

    public static implicit operator T(ConfigOption<T> option) => option.Value;
}
