using Retail_Api.Helper;

namespace Retail_Front.Service
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly string URL = "http://localhost:5082/api/Category";

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/GetAllCategories");
            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<IEnumerable<Category>>();

                return categories;
            }
            else throw new HttpRequestException($"Failed to Get all product  . Status code: {response.StatusCode}");

        }

        public async Task<ResponesBody<CategoryDto>> Add(CategoryDto categoryDto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{URL}/AddCategory", categoryDto);
            if (response.IsSuccessStatusCode)
            {
                return new ResponesBody<CategoryDto> { entity = categoryDto, IsTure = true, Massage = "category Added" };
            }
            else return new ResponesBody<CategoryDto> { entity = categoryDto, IsTure = false, Massage = $"{response.Content.ToString()}" }; ;


        }

        public async Task<bool> Update(int id, CategoryDto CategoryDto)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{URL}/UpdateCategory/{id}", CategoryDto);

            if (response.IsSuccessStatusCode)
            {
                //var createdProductJson = await response.Content.ReadAsStringAsync();
                return true;


            }
            //else throw new HttpRequestException($"Failed to create product. Status code: {response.StatusCode}");
            else return false;
        }
        public async Task<Category> Get(int id)
        {
            var categories = await GetAll();
            return categories.First(c => c.Id == id);
        }
        public async Task<bool> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{URL}/DeleteCategory/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;

        }
    }
}
