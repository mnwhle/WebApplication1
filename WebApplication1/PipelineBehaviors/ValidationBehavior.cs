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

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();
        if (failures.Any())
        {
            // Bad idea: throw new ValidationException(failures);
            var responseType = typeof(TResponse);
            var resultType = responseType.GetGenericArguments();
            var invalidResponseType = typeof(ValidateableResponse<>).MakeGenericType(resultType);
            Debug.WriteLine(invalidResponseType);
            object?[]? args = new object?[2] { null, failures };
            var invalidResponse = Activator.CreateInstance(invalidResponseType, args) as TResponse;
            Debug.Assert(invalidResponse is not null);
        }
        return next();
    }
}
