namespace WebApplication1.Responses;

public abstract class ValidateableResponseBase
{
    private readonly IList<string> _errors;

    public ValidateableResponseBase(IList<string>? errors)
    {
        _errors = errors ?? new List<string>();
    }

    public bool IsValid => !_errors.Any();
    public IReadOnlyCollection<string> Errors => new ReadOnlyCollection<string>(_errors);
    public string FormatErrors() => string.Join("\n", _errors);
}
