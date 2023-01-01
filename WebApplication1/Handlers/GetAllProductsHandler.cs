namespace WebApplication1.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, ValidateableResponse<List<ProductResponse>>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ValidateableResponse<List<ProductResponse>>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        var items = await _repository.SelectAsync(cancellationToken);
        return new ValidateableResponse<List<ProductResponse>>(_mapper.Map<List<ProductResponse>>(items));
    }
}
