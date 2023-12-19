using Retail_Api.Helper;
using Retail_Api.Models;
using Retail_Api.Repository;

namespace Retail_Api.Srevice
{
    public class CategoryService
    {
        private readonly categoryRepositry _categoryRepositry;
        public CategoryService(categoryRepositry categoryRepositry)
        {
            _categoryRepositry = categoryRepositry;
        }
        public IEnumerable<Category> GetAll()
        {
          return  _categoryRepositry.GetAll();
        }
        public ResponesBody<Category> AddCategory(CategoryDto categoryDto)
        {
            try
            {

            var category = new Category
            {
                Description = categoryDto.Description,
                Name = categoryDto.Name,
                
            };
            _categoryRepositry.Add(category);
            _categoryRepositry.save();
            return new ResponesBody<Category>() { IsTure =  true , entity = category , Massage = "category added"};
            }
             catch (Exception ex) { throw new Exception($"{ex}"); }
        }
        public ResponesBody<Category> UpdateCategory(int id,CategoryDto categoryDto)
        {
            try
            {
            var category = new Category
            {
                Id = id,
                Description = categoryDto.Description,
                Name = categoryDto.Name,
                
            };
            _categoryRepositry.update(category);
            _categoryRepositry.save();
            return new ResponesBody<Category>() { IsTure =  true , entity = category , Massage = "category updated"};
            }
             catch (Exception ex) { throw new Exception($"{ex}"); }
        }
        public ResponesBody<Category> DeleteCategory(int id)
        {
           var category = _categoryRepositry.Get(id);
            _categoryRepositry.remove(category);
            _categoryRepositry.save();
            return new ResponesBody<Category>() { IsTure = true, entity = category, Massage = "category added" };


        }
    }
}
