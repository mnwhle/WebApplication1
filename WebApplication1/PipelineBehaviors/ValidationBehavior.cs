namespace WebApplication1.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponce> : IPipelineBehavior<TRequest, TResponce>
    where TRequest : IRequest<TResponce>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponce> Handle(TRequest request, RequestHandlerDelegate<TResponce> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();
        if (failures.Any())
        {
            // Bad idea
            //throw new ValidationException(failures);
        }
        return next();
    }
}
