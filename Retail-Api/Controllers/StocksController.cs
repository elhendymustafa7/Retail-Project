using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Retail_Api.Srevice;

namespace Retail_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly StockService _stockService;

        public StocksController(StockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Stock>> GetAll() => Ok(_stockService.GetAll());

        [HttpPost("AddStock")]
        public ActionResult<Stock> AddStock([FromBody] StockDto stockDto)
        {
           return Ok(_stockService.Add(stockDto)); 
        }
        [HttpPut("UpdateStock/{id}")]
        public ActionResult Edit(int id, [FromBody] StockDto stockDto)
        {
            _stockService.Edit(id, stockDto);
            return Ok();
        }
        [HttpDelete("DeleteStock/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            _stockService.Delete(id);
            return Ok();
        }
    }
}
