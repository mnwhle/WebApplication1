namespace WebApplication1.Responces;

public class ValidateableResponce<TResponce> : ValidateableResponceBase
    where TResponce : class
{
    public ValidateableResponce(TResponce? model = null, IList<string>? errors = null) : base(errors)
    {
        Model = model;
    }

    public TResponce? Model { get; }
}