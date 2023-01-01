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
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();
        if (failures.Any())
        {
            // It is a possible but bad solution to throw an exception on validation failure.
            // Therefore we construct an invalid response here.
            var responseType = typeof(TResponse);
            var resultType = responseType.GetGenericArguments();
            var invalidResponseType = typeof(ValidateableResponse<>).MakeGenericType(resultType);
            Debug.WriteLine(invalidResponseType);
            //object?[]? args = new object?[] { null, failures };
            var invalidResponse = Activator.CreateInstance(invalidResponseType, null, failures.Select(x => x.ErrorMessage).ToList()) as TResponse;
            Debug.Assert(invalidResponse is not null);
            return invalidResponse;
        }
        else
        {
            return await next();
        }
    }
}
