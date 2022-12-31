namespace WebApplication1.Requests;

public class DeleteProductRequest : IRequest<ValidateableResponce<BooleanResponce>>
{
    public int Id { get; private set; }

    public DeleteProductRequest(int id)
    {
        Id = id;
    }
}
