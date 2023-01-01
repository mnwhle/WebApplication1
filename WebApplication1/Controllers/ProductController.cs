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
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsValid)
        {
            return BadRequest(response.FormatErrors());
        }
        return Ok(response);
    }

    [HttpGet(template: "{id}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
    {
        var request = new GetProductByIdRequest(id);
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsValid)
        {
            return BadRequest(response.FormatErrors());
        }
        return response is not null ? Ok(response) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] CreateProductRequest model, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(model, cancellationToken);
        if (!response.IsValid)
        {
            return BadRequest(response.FormatErrors());
        }
        return CreatedAtAction(nameof(GetProduct), routeValues: new { id = response?.Model?.Id, }, value: response?.Model);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest model, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(model, cancellationToken);
        if (!response.IsValid)
        {
            return BadRequest(response.FormatErrors());
        }
        return (response?.Model?.Success ?? false) ? Ok() : BadRequest();
    }

    [HttpDelete(template: "{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest(id);
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsValid)
        {
            return BadRequest(response.FormatErrors());
        }
        return (response?.Model?.Success ?? false) ? Ok() : BadRequest();
    }
}
