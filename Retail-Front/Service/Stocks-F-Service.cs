using Retail_Api.Helper;

namespace Retail_Front.Service
{
    public class Stocks_F_Service
    {
        private readonly HttpClient _httpClient;
        private readonly string URL = "http://localhost:5082/api/Stocks";

        public Stocks_F_Service(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Stock>> GetAll()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var stocks = await response.Content.ReadFromJsonAsync<IEnumerable<Stock>>();

                return stocks;
            }
            else throw new HttpRequestException($"Failed to Get all stocks  . Status code: {response.StatusCode}");

        }

        public async Task<ResponesBody<StockDto>> Create(StockDto StockDto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{URL}/AddStock", StockDto);
            if (response.IsSuccessStatusCode)
            {
                return new ResponesBody<StockDto> { entity = StockDto, IsTure = true, Massage = "stock Added" };
            }
            else return new ResponesBody<StockDto> { entity = StockDto, IsTure = false, Massage = $"{response.Content.ToString()}" }; ;


        }

        public async Task<bool> Update(int id, StockDto StockDto)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{URL}/UpdateStock/{id}", StockDto);

            if (response.IsSuccessStatusCode)
            {
                //var createdProductJson = await response.Content.ReadAsStringAsync();
                return true;


            }
            //else throw new HttpRequestException($"Failed to create product. Status code: {response.StatusCode}");
            else return false;
        }
        public async Task<bool> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{URL}/DeleteStock/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;

        }

        public async Task<Stock> Get(int id)
        {
            var stocks = await GetAll();
            return stocks.First(c => c.Id == id);
        }

    }
}
