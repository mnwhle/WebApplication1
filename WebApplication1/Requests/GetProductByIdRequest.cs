namespace WebApplication1.Requests;

public class GetProductByIdRequest : IRequest<ValidateableResponse<ProductResponse>>
{
    public int Id { get; private set; }

    public GetProductByIdRequest(int id)
    {
        Id = id;
    }
}
