using lab09_cc.Model;
using lab09_cc.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace lab09_cc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _productRepository.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var product = _productRepository.GetProductByID(id);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest("Product object is null");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _productRepository.InsertProduct(product);

                return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            try
            {
                if (product == null || product.Id != id)
                    return BadRequest("Invalid product object");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingProduct = _productRepository.GetProductByID(id);
                if (existingProduct == null)
                    return NotFound();

                _productRepository.UpdateProduct(product);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingProduct = _productRepository.GetProductByID(id);
                if (existingProduct == null)
                    return NotFound();

                _productRepository.DeleteProduct(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
