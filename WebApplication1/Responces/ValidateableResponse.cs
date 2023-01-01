namespace WebApplication1.Responses;

public class ValidateableResponse<TResponse> : ValidateableResponseBase
    where TResponse : class
{
    public ValidateableResponse(TResponse? model = null, IList<string>? errors = null) : base(errors)
    {
        Model = model;
    }

    public TResponse? Model { get; }
}