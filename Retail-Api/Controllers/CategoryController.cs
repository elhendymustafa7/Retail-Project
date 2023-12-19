using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Retail_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("GetAllCategories")]
        public ActionResult<IEnumerable<Category>> GetAllCategories() => Ok(_categoryService.GetAll());

        [HttpPost("AddCategory")]
        public ActionResult AddCategory(CategoryDto category) => Ok(_categoryService.AddCategory(category));
        [HttpPut("UpdateCategory/{id}")]
        public  IActionResult UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            return Ok(_categoryService.UpdateCategory(id,categoryDto));
        }
        [HttpDelete("DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
           return Ok(_categoryService.DeleteCategory(id));
        }


    }
}
