namespace WebApplication1.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ValidateableResponseBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(async x => await x.ValidateAsync(context))
            .SelectMany(x => x.Result.Errors)
            .Where(x => x is not null)
            .Select(x => x.ErrorMessage)
            .ToList();
        if (failures.Any())
        {
            // It is a possible solution but a bad idea to throw an exception here.
            // Therefore we construct an invalid response here.
            var responseType = typeof(TResponse);
            var resultType = responseType.GetGenericArguments();
            var invalidResponseType = typeof(ValidateableResponse<>).MakeGenericType(resultType);
            Debug.WriteLine(invalidResponseType);
            //object?[]? args = new object?[] { null, failures };
            var invalidResponse = Activator.CreateInstance(invalidResponseType, null, failures) as TResponse;
            Debug.Assert(invalidResponse is not null);
            return invalidResponse;
        }
        else
        {
            return await next();
        }
    }
}
