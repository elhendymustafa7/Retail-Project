namespace Retail_Api.Models
{
    public class Product_Stock
    {
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public string ProductName { get; set; }
        public Product Product { get; set; }
    }
}
