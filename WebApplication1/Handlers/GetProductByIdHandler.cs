namespace WebApplication1.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, ValidateableResponce<ProductResponce>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ValidateableResponce<ProductResponce>> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.SelectAsync(request.Id, cancellationToken);
        return new ValidateableResponce<ProductResponce>(_mapper.Map<ProductResponce>(item));
    }
}
