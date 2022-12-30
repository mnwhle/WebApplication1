namespace WebApplication1.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsRequest, List<ProductResponce>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductsHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductResponce>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
    {
        var items = await _repository.SelectAsync(cancellationToken);
        return _mapper.Map<List<ProductResponce>>(items);
    }
}
