namespace WebApplication1.Requests;

public class UpdateProductRequest : IRequest<EmptyResponce>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
