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
        var responce = await _mediator.Send(request, cancellationToken);
        if (!responce.IsValid)
        {
            return BadRequest(responce.FormatErrors());
        }
        return Ok(responce);
    }

    [HttpGet(template: "{id}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
    {
        var request = new GetProductByIdRequest(id);
        var responce = await _mediator.Send(request, cancellationToken);
        if (!responce.IsValid)
        {
            return BadRequest(responce.FormatErrors());
        }
        return responce is not null ? Ok(responce) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductRequest model, CancellationToken cancellationToken)
    {
        var responce = await _mediator.Send(model, cancellationToken);
        if (!responce.IsValid)
        {
            return BadRequest(responce.FormatErrors());
        }
        return CreatedAtAction(nameof(GetProduct), routeValues: new { id = responce?.Model?.Id, }, value: responce?.Model);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest model, CancellationToken cancellationToken)
    {
        var responce = await _mediator.Send(model, cancellationToken);
        if (!responce.IsValid)
        {
            return BadRequest(responce.FormatErrors());
        }
        return (responce?.Model?.Success ?? false) ? Ok() : BadRequest();
    }

    [HttpDelete(template: "{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest(id);
        var responce = await _mediator.Send(request, cancellationToken);
        if (!responce.IsValid)
        {
            return BadRequest(responce.FormatErrors());
        }
        return (responce?.Model?.Success ?? false) ? Ok() : BadRequest();
    }
}
