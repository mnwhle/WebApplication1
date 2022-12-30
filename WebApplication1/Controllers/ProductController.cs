namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
    {
        var request = new GetAllProductsRequest();
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet(template: "{id}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
    {
        var request = new GetProductByIdRequest(id);
        var result = await _mediator.Send(request, cancellationToken);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] Product model, CancellationToken cancellationToken)
    {
        var request = _mapper.Map<CreateProductRequest>(model);
        var result = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetProduct), routeValues: new { id = result.Id, }, value: result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Product model, CancellationToken cancellationToken)
    {
        var request = _mapper.Map<UpdateProductRequest>(model);
        var result = await _mediator.Send(request, cancellationToken);
        return result is not null ? Ok() : NotFound();
    }

    [HttpDelete(template: "{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest(id);
        var result = await _mediator.Send(request, cancellationToken);
        return result is not null ? Ok() : NotFound();
    }
}
