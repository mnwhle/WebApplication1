namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
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
    public async Task<IActionResult> AddProduct([FromBody] CreateProductRequest model, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(model, cancellationToken);
        return CreatedAtAction(nameof(GetProduct), routeValues: new { id = result.Id, }, value: result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest model, CancellationToken cancellationToken)
    {
        var responce = await _mediator.Send(model, cancellationToken);
        return responce.Success ? Ok() : BadRequest();
    }

    [HttpDelete(template: "{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest(id);
        var responce = await _mediator.Send(request, cancellationToken);
        return responce.Success ? Ok() : BadRequest();
    }
}
