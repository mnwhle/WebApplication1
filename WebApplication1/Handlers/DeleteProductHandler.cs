namespace WebApplication1.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, EmptyResponce>
{
    private readonly IProductRepository _repository;

    public DeleteProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmptyResponce> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
        return new EmptyResponce();
    }
}
