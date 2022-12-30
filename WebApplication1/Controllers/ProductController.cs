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
        var query = new GetAllProductsRequest();
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet(template: "{id}")]
    public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken)
    {
        var query = new GetProductByIdRequest(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] Product dto, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateProductRequest>(dto);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetProduct), routeValues: new { id = result.Id, }, value: result);
    }

    //[HttpPost, Route("")]
    //public async Task<IActionResult> Update(Product model, CancellationToken cancellationToken)
    //{
    //    var result = await _products.UpdateAsync(model, cancellationToken);
    //    if (result)
    //        return Ok("Product updated successfully");
    //    else
    //        return BadRequest("Product couldn't be updated");
    //}

    //[HttpDelete, Route("")]
    //public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    //{
    //    var result = await _products.DeleteAsync(id, cancellationToken);
    //    if (result)
    //        return Ok("Product deleted successfully");
    //    else
    //        return BadRequest("Product couldn't be deleted");
    //}
}
