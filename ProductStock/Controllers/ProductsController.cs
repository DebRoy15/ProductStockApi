using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductStockApi.Model.Dto;
using ProductStockApi.Model.Entities;
using ProductStockApi.Services;

namespace ProductStockApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _productService.GetProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            var product = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductDto productDto)
        {
            await _productService.UpdateProductAsync(id, productDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        [HttpPut("decrement-quantity/{id}/{quantity}")]
        public async Task<IActionResult> DecrementQuantity(Guid id, int quantity)
        {
            await _productService.DecrementQuantityAsync(id, quantity);
            return Ok();
        }

        [HttpPut("add-to-quantity/{id}/{quantity}")]
        public async Task<IActionResult> AddToQuantity(Guid id, int quantity)
        {
            await _productService.AddToQuantityAsync(id, quantity);
            return Ok();
        }
    }
}
