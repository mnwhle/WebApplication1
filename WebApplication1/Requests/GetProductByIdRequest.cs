namespace WebApplication1.Requests;

public class GetProductByIdRequest : IRequest<ProductResponce>
{
    public int Id { get; private set; }

    public GetProductByIdRequest(int id)
    {
        Id = id;
    }
}
