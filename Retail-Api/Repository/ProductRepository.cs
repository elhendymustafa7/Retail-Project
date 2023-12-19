global using Retail_Api.DTO;
using Retail_Api.Models;

namespace Retail_Api.Repository
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<Product> _logger;

        public ProductRepository(ApplicationDbContext context,ILogger<Product> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.AsNoTracking().ToListAsync();
        public async Task<Product> GetProductByNameAsync(string name) => await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Name == name);
        public async Task<Product> GetProductByNameWithTrackedAsync(string name) => await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        public void Add(Product entity) => _context.Products.Add(entity);
        public void Add(Product_Stock product_Stock) => _context.Product_Stock.Add(product_Stock);
        public int GetStockID(string name) => _context.Product_Stock.AsNoTracking().FirstOrDefault(p => p.ProductName == name).StockId;
        public void Update(Product entity) => _context.Products.Update(entity);
        public void Update(Product_Stock entity) => _context.Product_Stock.Update(entity);
        public  void DeleteAsync(Product entity) => _context.Products.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync(); 
        
    }
}
