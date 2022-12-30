namespace WebApplication1.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductRequest, ProductResponce>
{
    private readonly ILogger<CreateProductHandler> _logger;
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public CreateProductHandler(ILogger<CreateProductHandler> logger, IProductRepository repository, IMapper mapper)
    {
        _logger = logger;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductResponce> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Product>(request);
        var result = await _repository.InsertAsync(model, cancellationToken);
        _logger.LogInformation($"Created product: {result?.Id}, {result?.Name}");
        return _mapper.Map<ProductResponce>(result);
    }
}
