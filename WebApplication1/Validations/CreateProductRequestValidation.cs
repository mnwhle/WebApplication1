namespace WebApplication1.Validations;

public class CreateProductRequestValidation : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
