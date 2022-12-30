namespace WebApplication1.Requests;

public class GetProductByIdRequest : IRequest<ProductResponce>
{
    public Guid Id { get; private set; }

    public GetProductByIdRequest(Guid id)
    {
        Id = id;
    }
}
