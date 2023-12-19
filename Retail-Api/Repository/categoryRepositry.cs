using Microsoft.EntityFrameworkCore;

namespace Retail_Api.Repository
{
    public class categoryRepositry
    {
        private readonly ApplicationDbContext _context;

        public categoryRepositry(ApplicationDbContext context)
        {
            _context = context;
        }
        public  IEnumerable<Category> GetAll() =>  _context.Categories.AsNoTracking().ToList();
        public Category Get(int id) =>  _context.Categories.Find(id);
        public void Add(Category category) => _context.Categories.Add(category);
        public void update(Category category) => _context.Categories.Update(category);
        public void remove(Category category) => _context.Categories.Remove(category);
        public void save() => _context.SaveChanges();

    }
}
