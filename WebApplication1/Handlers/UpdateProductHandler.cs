namespace WebApplication1.Handlers;

public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, EmptyResponce>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EmptyResponce> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Product>(request);
        await _repository.UpdateAsync(model, cancellationToken);
        return new EmptyResponce();
    }
}
