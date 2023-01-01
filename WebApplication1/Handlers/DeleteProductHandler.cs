namespace WebApplication1.Handlers;

public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, ValidateableResponse<BooleanResponse>>
{
    private readonly ILogger<DeleteProductHandler> _logger;
    private readonly IProductRepository _repository;

    public DeleteProductHandler(ILogger<DeleteProductHandler> logger, IProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<ValidateableResponse<BooleanResponse>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var success = await _repository.DeleteAsync(request.Id, cancellationToken);
        _logger.LogInformation($"Deleted product: {request.Id}");
        return new ValidateableResponse<BooleanResponse>(new BooleanResponse(success));
    }
}
