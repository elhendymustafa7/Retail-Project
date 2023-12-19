global using Microsoft.EntityFrameworkCore;

namespace Retail_Api.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product_Stock>()
                .HasKey(p => new { p.StockId, p.ProductName });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product_Stock> Product_Stock { get; set; }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<AuditTrail> AuditTrail { get; set; }

        //seed category

    }
}
