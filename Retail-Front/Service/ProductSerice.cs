global using Retail_Api.Models;
using Retail_Api.Helper;
using System;

namespace Retail_Front.Service
{
    public class ProductSerice
    {
        private readonly HttpClient _httpClient;
        private readonly string URL = "http://localhost:5082/api/Product";

        public ProductSerice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public  async Task<List<Product>> GetAllProduct() 
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/GetAllProducts");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadFromJsonAsync<List<Product>>();

                return products;
            }
            else throw new HttpRequestException($"Failed to Get all product  . Status code: {response.StatusCode}");

        }
        public  async Task<ProductDto> GetProductByName(string productName) 
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{URL}/GetProductByName/{productName}");
            if (response.IsSuccessStatusCode)
            {
                var productDto = await response.Content.ReadFromJsonAsync<ProductDto>();
                return productDto;
            }
            else throw new HttpRequestException($"Failed to Get By product Name . Status code: {response.StatusCode}");

        }
        public  async Task<bool> UpdateProduct(ProductDto product) 
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{URL}/UpdateProduct", product);

            if (response.IsSuccessStatusCode)
            {
                return true;


            }
            else return false;
        }
        public  async Task<bool> DeleteProduct(string productName) 
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"{URL}/DeleteProduct?productName={Uri.EscapeDataString(productName)}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else return false;

        }
        public  async Task<ResponesBody<ProductDto>> AddProduct(ProductDto productDto) 
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{URL}/AddProduct", productDto);
            if (response.IsSuccessStatusCode)
            {
                return new ResponesBody<ProductDto> { entity = productDto , IsTure = true , Massage = "Product Added"};
            }
            else return new ResponesBody<ProductDto> { entity = productDto, IsTure = false, Massage = $"{response.Content.ToString()}" }; ;


        }
          public  async Task<IEnumerable<Stock>> GetAllStocks() 
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5082/api/Stocks/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var stocks = await response.Content.ReadFromJsonAsync<List<Stock>>();

                return stocks;
            }
            else throw new HttpRequestException($"Failed to Get all stocks  . Status code: {response.StatusCode}");

        }

    }
}
