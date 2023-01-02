namespace WebApplication1.Validations;

public class UpdateProductRequestValidation : AbstractValidator<UpdateProductRequest>
{
    private readonly IProductRepository _repository;

    public UpdateProductRequestValidation(IProductRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x)
            .MustAsync(async (x, cancellationToken) => await IsNameAlreadyExistsExcludeId(x.Id, x.Name, default))
            .WithMessage((u, name) => $"Product '{name}' already exists.");
    }

    private async Task<bool> IsNameAlreadyExistsExcludeId(int id, string name, CancellationToken cancellationToken)
    {
        bool exist = await _repository.IsNameAlreadyExistsExcludeIdAsync(id, name, cancellationToken);
        return !exist;
    }
}
