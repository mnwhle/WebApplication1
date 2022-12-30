namespace WebApplication1.Requests;

public class UpdateProductRequest : IRequest<BooleanResponce>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
