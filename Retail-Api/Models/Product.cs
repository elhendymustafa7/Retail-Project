using System.ComponentModel.DataAnnotations;

namespace Retail_Api.Models
{
    public class Product
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public List<Product_Stock> ProductStocks { get; set; }

    }
}
