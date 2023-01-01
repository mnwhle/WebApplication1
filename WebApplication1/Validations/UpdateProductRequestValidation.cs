namespace WebApplication1.Validations;

public class UpdateProductRequestValidation : AbstractValidator<UpdateProductRequest>
{
    private readonly IProductRepository _repository;

    public UpdateProductRequestValidation(IProductRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Name)
            .NotEmpty()
            .MustAsync(async (name, token) => await CheckNameIsNotExist(name, default))
            .WithMessage((u, name) => $"Product '{name}' already exists.");
    }

    private async Task<bool> CheckNameIsNotExist(string name, CancellationToken cancellationToken)
    {
        bool exist = await _repository.IsNameAlreadyExistsAsync(name, cancellationToken);
        return !exist;
    }
}
