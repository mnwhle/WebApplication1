namespace WebApplication1.Validations;

public class UpdateProductRequestValidation : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
