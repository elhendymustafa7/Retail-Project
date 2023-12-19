global using Retail_Api.Srevice;
using Microsoft.AspNetCore.Mvc;

namespace Retail_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController( ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetProductByName/{name}")]
        public async Task<ActionResult<ProductDto>> GetProductByName(string name)  
        {
            var result = await _productService.GetProductByNameAsync(name);
            if (result.IsTure) return Ok(result.entity); 
            return NotFound(result.Massage);
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts() 
        {
            return Ok(await _productService.GetProducts());
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<ProductDto>> AddProduct([FromBody] ProductDto productDto)
        {
            var result = await _productService.AddProduct(productDto);
            if (result.IsTure) return Ok(productDto);
            return BadRequest(result.Massage);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            var result = await _productService.UpdateProduct(productDto);
            if(result.IsTure) return Ok(productDto);
            return BadRequest(result.Massage);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(string productName)
        {
            var result = await _productService.DeleteProduct(productName);

            if (!result.IsTure) return NotFound(result.Massage);

            return Ok(result.Massage);
        }




    }
}
