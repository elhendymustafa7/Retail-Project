namespace Retail_Api.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
        public int Quantity { get; set; }
        public int CategoryID { get; set; }

        public int stockId { get; set; }
        //public string stockName { get; set; }
        //public int stockcapacity { get; set; }
        //public string stockAddress { get; set; }
    }
}
