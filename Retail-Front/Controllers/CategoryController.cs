using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Retail_Api.DTO;
using Retail_Api.Srevice;
using Retail_Front.Service;

namespace Retail_Front.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Retail_Front.Service.CategoryService _categoryService;

        public CategoryController(Retail_Front.Service.CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: CategoryController
        public async Task<ActionResult<IEnumerable<Category>>> Index()
        {
            var categories =await _categoryService.GetAll();
            return View(categories);
        }


        // GET: CategoryController/Create6+
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryDto categoryDto)
        {
            try
            {
               await _categoryService.Add(categoryDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
           
            return View(await _categoryService.Get(id));
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CategoryDto categoryDto)
        {
            try
            {
                if (await _categoryService.Update(id, categoryDto))
                    return RedirectToAction(nameof(Index));
                else return RedirectToAction("Error", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (await _categoryService.Delete(id))
                return RedirectToAction(nameof(Index));
            else return RedirectToAction("Error", "Home");
        }

        
    }
}
