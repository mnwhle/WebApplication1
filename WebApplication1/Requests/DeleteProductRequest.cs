namespace WebApplication1.Requests;

public class DeleteProductRequest : IRequest<EmptyResponce>
{
    public int Id { get; private set; }

    public DeleteProductRequest(int id)
    {
        Id = id;
    }
}
