namespace WebApplication1.Responces;

public abstract class ValidateableResponceBase
{
    private readonly IList<string> _errorMessages;

    public ValidateableResponceBase(IList<string>? errors)
    {
        _errorMessages = errors ?? new List<string>();
    }

    public bool IsValid => !_errorMessages.Any();
    public IReadOnlyCollection<string> Errors => new ReadOnlyCollection<string>(_errorMessages);
    public string FormatErrors() => string.Join("\n", _errorMessages);
}
