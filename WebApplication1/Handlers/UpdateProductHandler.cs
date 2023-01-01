namespace WebApplication1.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, ValidateableResponse<BooleanResponse>>
{
    private readonly ILogger<UpdateProductHandler> _logger;
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductHandler(ILogger<UpdateProductHandler> logger, IProductRepository repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ValidateableResponse<BooleanResponse>> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Product>(request);
        var success = await _repository.UpdateAsync(model, cancellationToken);
        _logger.LogInformation($"Updated product: {request.Id}");
        return new ValidateableResponse<BooleanResponse>(new BooleanResponse(success));
    }
}
