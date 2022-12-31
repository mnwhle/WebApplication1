namespace WebApplication1.Handlers;

public class CreateProductHandler : IRequestHandler<CreateProductRequest, ValidateableResponce<ProductResponce>>
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

    public async Task<ValidateableResponce<ProductResponce>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Product>(request);
        var result = await _repository.InsertAsync(model, cancellationToken);
        if (result is null)
        {
            return new ValidateableResponce<ProductResponce>();
        }
        model.Id = result.Value;
        _logger.LogInformation($"Created product: {model.Id}, {model.Name}");
        return new ValidateableResponce<ProductResponce>(_mapper.Map<ProductResponce?>(model));
    }
}
