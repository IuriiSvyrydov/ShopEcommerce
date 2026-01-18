


namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductQuery(catalogSpecParams);
            var products = await _mediator.Send(query);
            return Ok(products);

        }
        [HttpGet("GetProductsByName/{productName}")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductsByName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var products = await _mediator.Send(query);
            return Ok(products);
        }
        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }
        [HttpGet("GetProductsByBrand/{brandId}")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductsByBrand(string brandId)
        {
            var query = new GetProductByBrandQuery(brandId);
            var products = await _mediator.Send(query);
            if (products == null || !products.Any())
                return NotFound();
            var productList = products.Select(p => p.ToDto())
                .ToList();
            return Ok(productList);
        }
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var product = await _mediator.Send(productCommand);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        [HttpPut("UpdateProduct/{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(string id, [FromBody] UpdateProductCommand productCommand)
        {
            var command = productCommand.ToCommand(id);
            var updatedProduct = await _mediator.Send(command);
            if(!updatedProduct)
            {
                return NotFound();
            }
            return Ok(updatedProduct);  

        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand(id);
          var result =   await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpGet("GetBrands")]
        public async Task<ActionResult<IList<BrandDto>>> GetBrands()
        {
            var query = new GetAllBrandQuery();
            var brands = await _mediator.Send(query);
            return Ok(brands);
        }
        [HttpGet("GetTypes")]
        public async Task<ActionResult<IList<TypeDto>>> GetTypes()
        {
            var query = new GetAllTypeQuery();
            var types = await _mediator.Send(query);
            return Ok(types);
        }
    }
}
