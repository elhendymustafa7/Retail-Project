global using Retail_Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Retail_Api.Models;
using Retail_Front.Models;
using Retail_Front.Service;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace Retail_Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly string URL = "http://localhost:5082/api/Product";
        private readonly ProductSerice _productSerice;
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, ProductSerice productSerice)
        {
            _logger = logger;
            _httpClient = httpClient;
            _productSerice = productSerice;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productSerice.GetAllProduct();
            return View(products);

        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var stockIds = await _productSerice.GetAllStocks();
            
            ViewBag.StockItems = GetAllStocks(stockIds);

            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            var result = await _productSerice.AddProduct(productDto);
            
            if (result.IsTure)
            {
               
                return RedirectToAction(nameof(Index));
            }
            else return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string productName)
        {
           var product = await _productSerice.GetProductByName(productName);
            var stockIds = await _productSerice.GetAllStocks();

            ViewBag.StockItems = GetAllStocks(stockIds);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDto product)
        {
            if (await _productSerice.UpdateProduct(product)) return RedirectToAction(nameof(Index));
            else return RedirectToAction(nameof(Error));
            
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string productName)
        {
            
            if(await _productSerice.DeleteProduct(productName)) return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Error));
        }

        [HttpGet] //////////////////////////////////////////
        public async Task<IActionResult> Error(string Massage)
        {
            ViewBag.Massage = Massage;
            return View();
        }

        private Dictionary<int, string>   GetAllStocks(IEnumerable<Stock> stockIds)
        {
            Dictionary<int, string> items = new Dictionary<int, string>();
            foreach (var s in stockIds)
                items.Add(s.Id, s.Name);
            return items;

        }

    }
}
