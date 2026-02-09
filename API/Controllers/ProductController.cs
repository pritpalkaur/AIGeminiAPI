using API.BusinessLayer;
using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Product ID mismatch.");
            }

            var existingProduct = _productService.GetProductById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productService.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}