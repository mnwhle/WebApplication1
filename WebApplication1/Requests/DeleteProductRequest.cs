namespace WebApplication1.Requests;

public class DeleteProductRequest : IRequest<ValidateableResponse<BooleanResponse>>
{
    public int Id { get; private set; }

    public DeleteProductRequest(int id)
    {
        Id = id;
    }
}
