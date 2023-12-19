namespace Retail_Api.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int capacity { get; set; }
        public string Address { get; set; }
        public List<Product_Stock> product_Stocks { get; set; }

    }
}
