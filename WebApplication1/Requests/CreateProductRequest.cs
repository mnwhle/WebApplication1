namespace WebApplication1.Requests;

public class CreateProductRequest : IRequest<ProductResponce>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
