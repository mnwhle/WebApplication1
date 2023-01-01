namespace WebApplication1.Requests;

public class CreateProductRequest : IRequest<ValidateableResponse<ProductResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
