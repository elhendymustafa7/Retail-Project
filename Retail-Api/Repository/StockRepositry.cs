namespace Retail_Api.Repository
{
    public class StockRepositry
    {
        private readonly ApplicationDbContext _context;

        public StockRepositry(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Stock> GetAll() => _context.stocks.AsNoTracking().ToList();
        public Stock Get(int id) => _context.stocks.Find(id);
        public void Add(Stock stock) => _context.stocks.Add(stock);
        public void Edit(Stock stock) => _context.stocks.Update(stock);
        public void Remove(Stock stock) => _context.stocks.Remove(stock);
        public void save() => _context.SaveChanges();

        
    }
}
