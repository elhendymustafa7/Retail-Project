using Microsoft.AspNetCore.Mvc;
using Retail_Api.Srevice;
using Retail_Front.Service;

namespace Retail_Front.Controllers
{
    public class StocksController : Controller
    {
        private readonly Stocks_F_Service _stockService;

        public StocksController(Stocks_F_Service stockService)
        {
            _stockService = stockService;
        }

        public async Task<ActionResult<IEnumerable<Stock>>> Index()
        {
            var stocks =await _stockService.GetAll();
            return View(stocks);
        }
        public ActionResult Create()   
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StockDto StockDto)
        {
            try
            {
                await _stockService.Create(StockDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
         [HttpGet]
        public async Task<ActionResult> Editx(int id)
        {

            return View(await _stockService.Get(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editx(int id, StockDto StockDto)
        {
            try
            {
                if (await _stockService.Update(id, StockDto))
                    return RedirectToAction(nameof(Index));
                else return RedirectToAction("Error", "Home");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (await _stockService.Delete(id))
                return RedirectToAction(nameof(Index));
            else return RedirectToAction("Error", "Home");
        }

    }
}
